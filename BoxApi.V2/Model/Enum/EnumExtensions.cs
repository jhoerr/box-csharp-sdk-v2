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
    }
}