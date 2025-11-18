namespace Core.Utils.Localization;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class LocalizablePropAttribute : Attribute
{
    public string? Key { get; }
    public LocalizablePropAttribute(string? key = null)
    {
        Key = key;
    }
}
