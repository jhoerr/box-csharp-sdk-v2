using System.ComponentModel;

namespace BoxApi.V2.Model.Enum
{
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
}