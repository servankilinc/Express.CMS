namespace Core.Utils.CrossCuttingConcerns.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LocalizablePropAttribute : Attribute
    {
        public string? Key { get; }
        public LocalizablePropAttribute(string? key)
        {
            Key = key;
        }
    }
}
