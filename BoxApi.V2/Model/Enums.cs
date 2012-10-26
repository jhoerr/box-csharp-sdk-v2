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
        [Description("collaborations")] Collaboration,
        [Description("error")] Error,
    }

    public enum Field
    {
        [Description("created_at")] CreatedAt,
        [Description("created_by")] CreatedBy,
        [Description("description")] Description,
        [Description("etag")] Etag,
        [Description("expires_at")] ExpiresAt,
        [Description("is_reply_comment")] IsReplyComment,
        [Description("message")] Message,
        [Description("modified_at")] ModifiedAt,
        [Description("modified_by")] ModifiedBy,
        [Description("name")] Name,
        [Description("owned_by")] OwnedBy,
        [Description("parent")] Parent,
        [Description("path")] Path,
        [Description("path_id")] PathId,
        [Description("role")] Role,
        [Description("sequence_id")] SequenceId,
        [Description("shared_link")] SharedLink,
        [Description("size")] Size,
    }

    public enum Role
    {
        [Description("Editor")] Editor,
        [Description("Viewer")] Viewer,
        [Description("Previewer")] Previewer,
        [Description("Uploader")] Uploader,
        [Description("Previewer-Uploader")] PreviewerUploader,
        [Description("Viewer-Uploader")] ViewerUploader,
        [Description("Co-Owner")] CoOwner,
    }

    public enum Status
    {
        [Description("pending")] Pending,
        [Description("accepted")] Accepted,
        [Description("rejected")] Rejected,
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