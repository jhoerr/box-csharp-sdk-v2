using System.ComponentModel;

namespace BoxApi.V2.Model.Enum
{
    /// <summary>
    ///     Specifies which properties of an object should be returned with the RESTful response. They can be any fields that are a part of  the complete object for that particular type. Type and ID are always returned regardless of which fields are requested.
    /// </summary>
    public enum Field
    {
        [Description("acknowledged_at")] AcknowledgedAt,
        [Description("accessible_by")] AccessibleBy,
        [Description("address")] Address,
        [Description("avatar_url")] AvatarUrl,
        [Description("can_see_managed_users")] CanSeeManagedUsers,
        [Description("created_at")] CreatedAt,
        [Description("created_by")] CreatedBy,
        [Description("description")] Description,
        [Description("etag")] Etag,
        [Description("expires_at")] ExpiresAt,
        [Description("item_collection")] ItemCollection,
        [Description("is_reply_comment")] IsReplyComment,
        [Description("item")] Item,
        [Description("item_status")] ItemStatus,
        [Description("job_title")] JobTitle,
        [Description("language")] Language,
        [Description("login")] Login,
        [Description("max_upload_size")] MaxUploadSize,
        [Description("message")] Message,
        [Description("modified_at")] ModifiedAt,
        [Description("modified_by")] ModifiedBy,
        [Description("name")] Name,
        [Description("owned_by")] OwnedBy,
        [Description("parent")] Parent,
        [Description("path")] Path,
        [Description("path_id")] PathId,
        [Description("phone")] Phone,
        [Description("role")] Role,
        [Description("sequence_id")] SequenceId,
        [Description("shared_link")] SharedLink,
        [Description("size")] Size,
        [Description("space_amount")] SpaceAmount,
        [Description("space_used")] SpaceUsed,
        [Description("status")] Status,
        [Description("is_sync_enabled")] IsSyncEnabled,
        [Description("tracking_codes")] TrackingCodes,
        [Description("is_exempt_from_login_verification")]IsExemptFromLoginVerification,
        [Description("is_exempt_from_device_limits")]IsExemptFromDeviceLimits,
    }
}