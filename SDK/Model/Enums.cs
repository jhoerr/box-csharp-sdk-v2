using System;
using System.ComponentModel;

namespace BoxApi.V2.SDK.Model
{
    public enum Access
    {
        Open,
        Company,
        Collaborators,
    }

    public enum Type
    {
        [Description("files")] File,
        [Description("folders")] Folder,
        [Description("comments")] Comment,
        [Description("discussions")] Discussion,
        [Description("events")] Event,
        [Description("tokens")] Token,
        [Description("error")] Error,
    }

    public static class EnumExtensions
    {
        public static string Name(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        public static string Description(this Enum value)
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