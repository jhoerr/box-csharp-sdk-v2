using System;
using System.ComponentModel;

namespace BoxApi.V2.Model.Enum
{
    public static class EnumExtensions
    {
        public static string Name(this System.Enum value)
        {
            return System.Enum.GetName(value.GetType(), value);
        }

        public static string Description(this System.Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes =
                (DescriptionAttribute[]) fi.GetCustomAttributes(
                    typeof (DescriptionAttribute),
                    false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }
    }
}