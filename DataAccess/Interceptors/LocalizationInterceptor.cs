using Core.Model;
using Core.Utils.HttpContextManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DataAccess.Interceptors;

public sealed class LocalizationQueryInterceptor : IMaterializationInterceptor
{
    private readonly HttpContextManager _httpContextManager;
    public LocalizationQueryInterceptor(HttpContextManager httpContextManager) => _httpContextManager = httpContextManager;

    public object InitializedInstance(MaterializationInterceptionData materializationData, object entity)
    {
        if (entity is ILocalizableEntity)
        {
            var localizableProps = entity.GetType().GetProperties().Where(p => p.PropertyType == typeof(string) && Attribute.IsDefined(p, typeof(LocalizablePropAttribute)));
            if (localizableProps.Any())
            {
                foreach (var prop in localizableProps)
                {
                    var originalValue = prop.GetValue(entity) as string;
                    if (!string.IsNullOrEmpty(originalValue))
                    {
                        var localizedValue = originalValue;  // loaclization servisinden gelecek 
                        prop.SetValue(entity, localizedValue);
                    }
                }
            }
        }
        return entity;
    }
}


public sealed class LocalizationCommandInterceptor : SaveChangesInterceptor
{
    private readonly HttpContextManager _httpContextManager;
    public LocalizationCommandInterceptor(HttpContextManager httpContextManager) => _httpContextManager = httpContextManager;


    //  ****************************** SYNC VERSION ******************************
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null) return base.SavingChanges(eventData, result);

        IEnumerable<EntityEntry<ILocalizableEntity>> localizableEntries = eventData.Context.ChangeTracker.Entries<ILocalizableEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        if (localizableEntries.Any())
        {
            foreach (EntityEntry<ILocalizableEntity> entry in localizableEntries)
            {
                var localizableProps = entry.GetType().GetProperties().Where(p => p.PropertyType == typeof(string) && Attribute.IsDefined(p, typeof(LocalizablePropAttribute)));
                if (localizableProps.Any())
                {
                    foreach (var prop in localizableProps)
                    {
                        var originalValue = prop.GetValue(entry) as string;
                        if (!string.IsNullOrEmpty(originalValue))
                        {
                            if (entry.State == EntityState.Added)
                            {
                                var localizedValue = originalValue; // loaclization servisinden gelecek 
                                prop.SetValue(entry, localizedValue);

                                // 1.currentCulture Code bilgisini cookieden alıp db de hangi Id ye karşılık gelidği bulunmalı
                                // 2.localization
                                // 2.OriginalValue formdan gelen türkçe veri 
                            }
                            else if (entry.State == EntityState.Modified)
                            {

                            }
                        }
                    }
                }
            }
        }

        return base.SavingChanges(eventData, result);
    }


    //  ****************************** ASYNC VERSION ******************************
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null) return base.SavingChangesAsync(eventData, result, cancellationToken);

        IEnumerable<EntityEntry<ILocalizableEntity>> localizableEntries = eventData.Context.ChangeTracker.Entries<ILocalizableEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        if (localizableEntries.Any())
        {
            foreach (EntityEntry<ILocalizableEntity> entry in localizableEntries)
            {
                // ...
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}


[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class LocalizablePropAttribute : Attribute
{
}