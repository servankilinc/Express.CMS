using Core.Enums;
using Core.Model;
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
    private readonly List<Localization> _pendingLocalization = new();
    private readonly List<LocalizationLanguageDetail> _pendingLangDetails = new();

    public LocalizationCommandInterceptor(HttpContextManager httpContextManager) => _httpContextManager = httpContextManager;


    //  ****************************** SYNC VERSION ******************************
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null) return base.SavingChanges(eventData, result);

        var localizableEntries = eventData.Context.ChangeTracker.Entries<ILocalizableEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        if (!localizableEntries.Any()) return base.SavingChanges(eventData, result);

        var languageId = _httpContextManager.GetCurrentLanguageId();
        
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
                var existLocalization = eventData.Context.Set<Localization>().SingleOrDefault(f => f.Key == key);
                Guid localizationId;

                // 4) Varsa ilgili dildeki değeri güncelle yoksa yeni kayıt oluştur
                if (existLocalization != null)
                {
                    localizationId = existLocalization.Id;
                }
                else
                {
                    var pending = _pendingLocalization.FirstOrDefault(x => x.Key == key);
                    if (pending != null)
                    {
                        localizationId = pending.Id;
                    }
                    else
                    {
                        localizationId = Guid.NewGuid();
                        _pendingLocalization.Add(new Localization
                        {
                            Id = localizationId,
                            TableName = entry.GetTableName(),
                            EntityId = entry.GetEntityId(),
                            Key = key
                        });
                    }
                }
                _pendingLangDetails.Add(new LocalizationLanguageDetail
                {
                    LocalizationId = localizationId,
                    LanguageId = languageId,
                    Value = originalValue,
                });

                // 5) Entity üzerindeki değeri key ile değiştir
                // Replace entity property value with the key string (so DB stores key, not raw text)
                // We set on the tracked entry to preserve tracking behavior
                if (entry.Properties.Any(p => p.Metadata.Name == prop.Name))
                    entry.Property(prop.Name).CurrentValue = key;
                else
                    prop.SetValue(entry.Entity, key);
            }
        }

        return base.SavingChanges(eventData, result);
    }
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        if (eventData.Context is null) 
            return base.SavedChanges(eventData, result);
        if (!_pendingLocalization.Any() && !_pendingLangDetails.Any())
            return base.SavedChanges(eventData, result);


        eventData.Context.ChangeTracker.AutoDetectChangesEnabled = false;
        eventData.Context.Set<Localization>().AddRange(_pendingLocalization);
        eventData.Context.Set<LocalizationLanguageDetail>().AddRange(_pendingLangDetails);
        eventData.Context.SaveChanges();
        eventData.Context.ChangeTracker.AutoDetectChangesEnabled = true;
        
        _pendingLocalization.Clear();
        _pendingLangDetails.Clear();

        return base.SavedChanges(eventData, result);
    }

    //  ****************************** ASYNC VERSION ******************************
    //public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    //{
    //    if (eventData.Context is null) return base.SavingChangesAsync(eventData, result, cancellationToken);

    //    var localizableEntries = eventData.Context.ChangeTracker.Entries<ILocalizableEntity>()
    //        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

    //    if (localizableEntries.Any())
    //    {
    //        foreach (EntityEntry<ILocalizableEntity> entry in localizableEntries)
    //        {
    //            // ...
    //        }
    //    }

    //    return base.SavingChangesAsync(eventData, result, cancellationToken);
    //}

    public string GenerateLocalizationKey(string? tableName, string? propertyName, string? entityId)
    {
        return $"{tableName ?? "undefined"}_{propertyName ?? "undefined"}_{entityId ?? "undefined"}";
    }
}


[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class LocalizablePropAttribute : Attribute
{
    public string? Key { get; }
    public LocalizablePropAttribute(string? key)
    {
        Key = key;
    }
}