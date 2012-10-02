using System;
using System.ComponentModel;

namespace BoxApi.V2.Model
{
    public enum Access
    {
        Open,
        Company,
        Collaborators,
    }

    public enum ResourceType
    {
        [Description("files")] File,
        [Description("folders")] Folder,
        [Description("comments")] Comment,
        [Description("discussions")] Discussion,
        [Description("events")] Event,
        [Description("tokens")] Token,
        [Description("error")] Error,
    }

    public enum Field
    {
        [Description("created_at")] CreatedAt,
        [Description("created_by")] CreatedBy,
        [Description("description")] Description,
        [Description("etag")] Etag,
        [Description("modified_at")] ModifiedAt,
        [Description("modified_bu")] ModifiedBy,
        [Description("name")] Name,
        [Description("owned_by")] OwnedBy,
        [Description("parent")] Parent,
        [Description("path")] Path,
        [Description("path_id")] PathId,
        [Description("sequence_id")] SequenceId,
        [Description("shared_link")] SharedLink,
        [Description("size")] Size,
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