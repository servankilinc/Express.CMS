using Core.Enums;
using Core.Utils.Caching;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Model.ProjectEntities;

namespace DataAccess.Interceptors.Helpers;

public class LocalizationHelper
{
    private readonly ICacheService _cacheService;
    public LocalizationHelper(ICacheService cacheService) => _cacheService = cacheService;


    public string GenerateLocalizationKey(string? tableName, string? propertyName, string? entityId)
    {
        return $"{tableName ?? "undefined"}_{propertyName ?? "undefined"}_{entityId ?? "undefined"}";
    }

    public string? ResolveLocalizationValue(AppDbContext context, string key, int languageId)
    {
        //string cacheKey = $"localization-{key}-{languageId}";
        //string[] cacheGroups = ["localization-group"];

        //CacheResponse cachedLocalization = _cacheService.GetFromCache(cacheKey);
        //if (cachedLocalization.IsSuccess && !string.IsNullOrWhiteSpace(cachedLocalization.Source))
        //    return cachedLocalization.Source;

        var localization = context.Set<Localization>().Include(i => i.LocalizationLanguageDetails).FirstOrDefault(f => f.Key == key);

        if (localization == null || localization.LocalizationLanguageDetails == null) return null;

        var all = localization.LocalizationLanguageDetails.AsEnumerable();
        var langDetail =
            all.FirstOrDefault(x => x.LanguageId == languageId) ??
            all.FirstOrDefault(x => x.LanguageId == (byte)Languages.Turkish) ??
            all.FirstOrDefault();

        //if (langDetail != null && !string.IsNullOrWhiteSpace(langDetail.Value))
        //    _cacheService.AddToCache<string>(cacheKey, cacheGroups, langDetail.Value);

        return langDetail?.Value;
    }
}