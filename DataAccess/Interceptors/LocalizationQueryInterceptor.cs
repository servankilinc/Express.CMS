using Core.Model;
using Core.Utils.HttpContextManager;
using Core.Utils.Localization;
using DataAccess.Contexts;
using DataAccess.Interceptors.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Log = Serilog.Log;

namespace DataAccess.Interceptors;

public sealed class LocalizationQueryInterceptor : IMaterializationInterceptor
{
    private readonly HttpContextManager _httpContextManager;
    private readonly LocalizationHelper _localizationHelper;
    private readonly IDbContextFactory<AppDbContext> _contextFactory;
    public LocalizationQueryInterceptor(HttpContextManager httpContextManager, LocalizationHelper localizationHelper, IDbContextFactory<AppDbContext> contextFactory)
    {
        _httpContextManager = httpContextManager;
        _localizationHelper = localizationHelper;
        _contextFactory = contextFactory;
    }

    public object InitializedInstance(MaterializationInterceptionData materializationData, object entity)
    {
        if (entity is not ILocalizableEntity) return entity;

        var localizableProps = entity.GetType().GetProperties().Where(p => p.PropertyType == typeof(string) && Attribute.IsDefined(p, typeof(LocalizablePropAttribute)) && p.GetValue(entity) != null);
        if (!localizableProps.Any()) return entity;

        var languageId = _httpContextManager.GetCurrentLanguageId();

        foreach (var prop in localizableProps)
        {
            //var key = prop.GetValue(entity) as string;
            var attr = (LocalizablePropAttribute?)prop.GetCustomAttributes(typeof(LocalizablePropAttribute), false).FirstOrDefault();
            if (attr == null) continue;

            var key = attr.Key;
            if (string.IsNullOrWhiteSpace(key)) continue;
            using var context = _contextFactory.CreateDbContext();
            var localizedValue = _localizationHelper.ResolveLocalizationValue(context, key, languageId);
            if (localizedValue != null)
            {
                prop.SetValue(entity, localizedValue);
            }
            else
            {
                Log.Logger.Warning($"Localization not found for Key: {key} LanguageId: {languageId}");
            }
        }

        return entity;
    }
}
