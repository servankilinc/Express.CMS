using Core.Model;
using Core.Utils.HttpContextManager;
using Core.Utils.Localization;
using DataAccess.Interceptors.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Model.ProjectEntities;
using System.Reflection;

namespace DataAccess.Interceptors;


public sealed class LocalizationCommandInterceptor : SaveChangesInterceptor
{
    private readonly HttpContextManager _httpContextManager;
    private readonly LocalizationHelper _localizationHelper;
    public LocalizationCommandInterceptor(HttpContextManager httpContextManager, LocalizationHelper localizationHelper)
    {
        _httpContextManager = httpContextManager;
        _localizationHelper = localizationHelper;
    }


    //  ****************************** SYNC VERSION ******************************
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null) return base.SavingChanges(eventData, result);

        var localizableEntries = eventData.Context.ChangeTracker.Entries<ILocalizableEntity>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        if (localizableEntries.Any())
            ProcessLocalization(eventData.Context, localizableEntries);

        return base.SavingChanges(eventData, result);
    }
    private void ProcessLocalization(DbContext context, IEnumerable<EntityEntry<ILocalizableEntity>> localizableEntries)
    {
        var languageId = _httpContextManager.GetCurrentLanguageId();
        var localizationSet = context.Set<Localization>();
        var langDetailSet = context.Set<LocalizationLanguageDetail>();

        foreach (EntityEntry<ILocalizableEntity> entry in localizableEntries)
        {
            var localizableProps = GetLocalizableProps(entry);

            foreach (var prop in localizableProps)
            {
                // 1) Mevcutta gelen değeri al
                var originalValue = prop.GetValue(entry.Entity) as string;
                if (string.IsNullOrWhiteSpace(originalValue)) continue;

                // 2) Key bilgi attr ile geliyorsa kullan yoksa oluştur
                var attr = (LocalizablePropAttribute?)prop.GetCustomAttributes(typeof(LocalizablePropAttribute), false).FirstOrDefault();
                var key = attr?.Key ?? _localizationHelper.GenerateLocalizationKey(entry.GetTableName(), prop.Name, entry.GetEntityId());

                // 3) Mevcutta bu key'e ait bir localization var mı kontrol et
                var existLocalization = localizationSet.Include(i => i.LocalizationLanguageDetails).FirstOrDefault(f => f.Key == key);

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
                        context.Entry(existLanguageLocalization).State = EntityState.Modified;
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

                //// 5) Entity üzerindeki değeri key ile değiştir
                //entry.Property(prop.Name).CurrentValue = key;
                //entry.Property(prop.Name).IsModified = true;
            }
        }
    }


    //  ****************************** ASYNC VERSION ******************************
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null) return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var localizableEntries = eventData.Context.ChangeTracker.Entries<ILocalizableEntity>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

        if (localizableEntries.Any())
            await ProcessLocalizationsAsync(eventData.Context, localizableEntries, cancellationToken);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    private async Task ProcessLocalizationsAsync(DbContext context, List<EntityEntry<ILocalizableEntity>> localizableEntries, CancellationToken cancellationToken)
    {
        var languageId = _httpContextManager.GetCurrentLanguageId();
        var localizationSet = context.Set<Localization>();
        var langDetailSet = context.Set<LocalizationLanguageDetail>();

        foreach (EntityEntry<ILocalizableEntity> entry in localizableEntries)
        {
            var localizableProps = GetLocalizableProps(entry);

            foreach (var prop in localizableProps)
            {
                // 1) Mevcutta gelen değeri al
                var originalValue = prop.GetValue(entry.Entity) as string;
                if (string.IsNullOrWhiteSpace(originalValue)) continue;

                // 2) Key bilgi attr ile geliyorsa kullan yoksa oluştur
                var attr = (LocalizablePropAttribute?)prop.GetCustomAttributes(typeof(LocalizablePropAttribute), false).FirstOrDefault();
                var key = attr?.Key ?? _localizationHelper.GenerateLocalizationKey(entry.GetTableName(), prop.Name, entry.GetEntityId());

                // 3) Mevcutta bu key'e ait bir localization var mı kontrol et
                var existLocalization = await localizationSet.Include(i => i.LocalizationLanguageDetails).FirstOrDefaultAsync(f => f.Key == key, cancellationToken);

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
                        context.Entry(existLanguageLocalization).State = EntityState.Modified;
                    }
                    // b) localizasyon bilgisi var fakat aktif dilde kaydı yoksa
                    else
                    {
                        await langDetailSet.AddAsync(new LocalizationLanguageDetail
                        {
                            LocalizationId = localizationId,
                            LanguageId = languageId,
                            Value = originalValue
                        }, cancellationToken);
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
                    }, cancellationToken);

                    await langDetailSet.AddAsync(new LocalizationLanguageDetail
                    {
                        LocalizationId = localizationId,
                        LanguageId = languageId,
                        Value = originalValue
                    }, cancellationToken);
                }

                //// 5) Entity üzerindeki değeri key ile değiştir
                //entry.Property(prop.Name).CurrentValue = key;
                //entry.Property(prop.Name).IsModified = true;
            }
        }
    }


    private List<PropertyInfo> GetLocalizableProps(EntityEntry<ILocalizableEntity> entry)
    {
        // İlgili entity üzerindeki localizable prop'ları bul ekleme işlemi ise bütün localizable prop'ları işle güncelleme ise sadece değişenleri işle
        return entry.Entity.GetType().GetProperties().Where(p =>
            p.PropertyType == typeof(string) &&
            Attribute.IsDefined(p, typeof(LocalizablePropAttribute)) &&
            p.GetValue(entry.Entity) != null &&
            (entry.State == EntityState.Added || entry.Property(p.Name).IsModified)
        ).ToList();
    }
}