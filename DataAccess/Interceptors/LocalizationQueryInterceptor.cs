using Core.Model;
using Core.Utils.HttpContextManager;
using Core.Utils.Localization;
using DataAccess.Interceptors.Helpers;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Log = Serilog.Log;

namespace DataAccess.Interceptors;

public sealed class LocalizationQueryInterceptor : IMaterializationInterceptor
{
    private readonly HttpContextManager _httpContextManager;
    private readonly LocalizationHelper _localizationHelper;
    public LocalizationQueryInterceptor(HttpContextManager httpContextManager, LocalizationHelper localizationHelper)
    {
        _httpContextManager = httpContextManager;
        _localizationHelper = localizationHelper;
    }

    public object InitializedInstance(MaterializationInterceptionData materializationData, object entity)
    {
        if (entity is not ILocalizableEntity) return entity;

        var localizableProps = entity.GetType().GetProperties().Where(p => p.PropertyType == typeof(string) && Attribute.IsDefined(p, typeof(LocalizablePropAttribute)) && p.GetValue(entity) != null);
        if (!localizableProps.Any()) return entity;

        var languageId = _httpContextManager.GetCurrentLanguageId();

        foreach (var prop in localizableProps)
        {
            var key = prop.GetValue(entity) as string;
            if (string.IsNullOrWhiteSpace(key)) continue;

            var localizedValue = _localizationHelper.ResolveLocalizationValue(materializationData.Context, key, languageId);
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
