using Core.Enums;
using Core.Model;
using Core.Utils.CrossCuttingConcerns.Attributes;
using Core.Utils.HttpContextManager;
using DataAccess.Interceptors.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Model.ProjectEntities;
using Serilog;
using System;

namespace DataAccess.Interceptors;

//public sealed class LocalizationQueryInterceptor : IMaterializationInterceptor
//{
//    private readonly HttpContextManager _httpContextManager;
//    public LocalizationQueryInterceptor(HttpContextManager httpContextManager) => _httpContextManager = httpContextManager;

//    public object InitializedInstance(MaterializationInterceptionData materializationData, object entity)
//    {
//        if (entity is ILocalizableEntity)
//        {
//            var localizableProps = entity.GetType().GetProperties().Where(p => p.PropertyType == typeof(string) && Attribute.IsDefined(p, typeof(LocalizablePropAttribute)));
//            if (localizableProps.Any())
//            {
//                foreach (var prop in localizableProps)
//                {
//                    var originalValue = prop.GetValue(entity) as string;
//                    if (!string.IsNullOrEmpty(originalValue))
//                    {
//                        var localizedValue = originalValue;  // loaclization servisinden gelecek 
//                        prop.SetValue(entity, localizedValue);
//                    }
//                }
//            }
//        }
//        return entity;
//    }
//}


public sealed class LocalizationCommandInterceptor : SaveChangesInterceptor
{
    private readonly HttpContextManager _httpContextManager;
    public LocalizationCommandInterceptor(HttpContextManager httpContextManager) => _httpContextManager = httpContextManager;


    //  ****************************** SYNC VERSION ******************************
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null) return base.SavingChanges(eventData, result);

        var localizableEntries = eventData.Context.ChangeTracker.Entries<ILocalizableEntity>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        if (!localizableEntries.Any()) return base.SavingChanges(eventData, result);

        var languageId = _httpContextManager.GetCurrentLanguageId();
        var localizationSet = eventData.Context.Set<Localization>();
        var langDetailSet = eventData.Context.Set<LocalizationLanguageDetail>();

        foreach (EntityEntry<ILocalizableEntity> entry in localizableEntries)
        {
            var localizableProps = entry.Entity.GetType().GetProperties().Where(p => p.PropertyType == typeof(string) && Attribute.IsDefined(p, typeof(LocalizablePropAttribute)));
            if (!localizableProps.Any()) continue;

            foreach (var prop in localizableProps)
            {
                // 1) Mevcutta gelen değeri al
                var originalValue = prop.GetValue(entry) as string;
                if (string.IsNullOrEmpty(originalValue)) continue;

                // 2) Key bilgi attr ile geliyorsa kullan yoksa oluştur
                var attr = (LocalizablePropAttribute?)prop.GetCustomAttributes(typeof(LocalizablePropAttribute), false).FirstOrDefault();
                var key = attr?.Key ?? GenerateLocalizationKey(entry.GetTableName(), prop.Name, entry.GetEntityId());

                // 3) Mevcutta bu key'e ait bir localization var mı kontrol et
                var existLocalization = localizationSet.Include(i => i.LocalizationLanguageDetails).SingleOrDefault(f => f.Key == key);
                Guid localizationId;

                // 4) Varsa ilgili dildeki değeri güncelle yoksa yeni kayıt oluştur
                if (existLocalization != null)
                {
                    localizationId = existLocalization.Id;

                    // a) localizasyon bilgisi var ve aktif dilde de kaydı var
                    var existLanguageLocalization = existLocalization.LocalizationLanguageDetails?.FirstOrDefault(f => f.LanguageId == languageId);
                    if (existLanguageLocalization != null)
                    {
                        existLanguageLocalization.Value = originalValue;
                        langDetailSet.Update(existLanguageLocalization);
                    }
                    // b) localizasyon bilgisi var fakat aktif dilde kaydı yoksa
                    else
                    {
                        langDetailSet.Add(new LocalizationLanguageDetail
                        {
                            LocalizationId = localizationId,
                            LanguageId = languageId,
                            Value = originalValue
                        });
                    }
                }
                else
                {
                    localizationId = Guid.NewGuid();

                    // a) localization kaydı ve localizationLanguageDetail kaydı açılmalı
                    localizationSet.Add(new Localization
                    {
                        Id = localizationId,
                        TableName = entry.GetTableName(),
                        EntityId = entry.GetEntityId(),
                        Key = key
                    });
                    langDetailSet.Add(new LocalizationLanguageDetail
                    {
                        LocalizationId = localizationId,
                        LanguageId = languageId,
                        Value = originalValue
                    });
                }

                // 5) Entity üzerindeki değeri key ile değiştir
                prop.SetValue(entry.Entity, key);
            }
        }

        return base.SavingChanges(eventData, result);
    }


    //  ****************************** ASYNC VERSION ******************************
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null) return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var localizableEntries = eventData.Context.ChangeTracker.Entries<ILocalizableEntity>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

        if (!localizableEntries.Any()) return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var languageId = _httpContextManager.GetCurrentLanguageId();
        var localizationSet = eventData.Context.Set<Localization>();
        var langDetailSet = eventData.Context.Set<LocalizationLanguageDetail>();

        foreach (EntityEntry<ILocalizableEntity> entry in localizableEntries)
        {
            var localizableProps = entry.Entity.GetType().GetProperties().Where(p => p.PropertyType == typeof(string) && Attribute.IsDefined(p, typeof(LocalizablePropAttribute)));
            if (!localizableProps.Any()) continue;

            foreach (var prop in localizableProps)
            {
                // 1) Mevcutta gelen değeri al
                var originalValue = prop.GetValue(entry) as string;
                if (string.IsNullOrEmpty(originalValue)) continue;

                // 2) Key bilgi attr ile geliyorsa kullan yoksa oluştur
                var attr = (LocalizablePropAttribute?)prop.GetCustomAttributes(typeof(LocalizablePropAttribute), false).FirstOrDefault();
                var key = attr?.Key ?? GenerateLocalizationKey(entry.GetTableName(), prop.Name, entry.GetEntityId());

                // 3) Mevcutta bu key'e ait bir localization var mı kontrol et
                var existLocalization = await localizationSet.Include(i => i.LocalizationLanguageDetails).SingleOrDefaultAsync(f => f.Key == key);
                Guid localizationId;

                // 4) Varsa ilgili dildeki değeri güncelle yoksa yeni kayıt oluştur
                if (existLocalization != null)
                {
                    localizationId = existLocalization.Id;

                    // a) localizasyon bilgisi var ve aktif dilde de kaydı var
                    var existLanguageLocalization = existLocalization.LocalizationLanguageDetails?.FirstOrDefault(f => f.LanguageId == languageId);
                    if (existLanguageLocalization != null)
                    {
                        existLanguageLocalization.Value = originalValue;
                        await langDetailSet.AddAsync(existLanguageLocalization);
                    }
                    // b) localizasyon bilgisi var fakat aktif dilde kaydı yoksa
                    else
                    {
                        await langDetailSet.AddAsync(new LocalizationLanguageDetail
                        {
                            LocalizationId = localizationId,
                            LanguageId = languageId,
                            Value = originalValue
                        });
                    }
                }
                else
                {
                    localizationId = Guid.NewGuid();

                    // a) localization kaydı ve localizationLanguageDetail kaydı açılmalı
                    await localizationSet.AddAsync(new Localization
                    {
                        Id = localizationId,
                        TableName = entry.GetTableName(),
                        EntityId = entry.GetEntityId(),
                        Key = key
                    });
                    await langDetailSet.AddAsync(new LocalizationLanguageDetail
                    {
                        LocalizationId = localizationId,
                        LanguageId = languageId,
                        Value = originalValue
                    });
                }

                // 5) Entity üzerindeki değeri key ile değiştir
                prop.SetValue(entry.Entity, key);
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
     


    public string GenerateLocalizationKey(string? tableName, string? propertyName, string? entityId)
    {
        return $"{tableName ?? "undefined"}_{propertyName ?? "undefined"}_{entityId ?? "undefined"}";
    }
}