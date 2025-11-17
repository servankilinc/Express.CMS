using System.ComponentModel;

namespace Core.Utils;

public class EnumExtensions
{
    public static T GetEnumByDescription<T>(string description) where T : Enum
    {
        foreach (var field in typeof(T).GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == description)
                    return (T)field.GetValue(null)!;
            }
            else
            {
                if (field.Name == description)
                    return (T)field.GetValue(null)!;
            }
        }

        throw new ArgumentException($"No enum with description '{description}' found in {typeof(T)}");
    }
}
