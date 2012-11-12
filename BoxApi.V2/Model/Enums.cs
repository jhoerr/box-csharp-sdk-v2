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
        [Description("users")] User,
        [Description("shared_items")] SharedItem,
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

    public enum StreamType
    {
        /// <summary>
        ///   Returns all event types
        /// </summary>
        [Description("all")] All,

        /// <summary>
        ///   Returns tree changes
        /// </summary>
        [Description("changes")] Changes,

        /// <summary>
        ///   Returns tree changes only for sync folders
        /// </summary>
        [Description("sync")] Sync,

        /// <summary>
        ///   Returns administrative events for Enterprise accounts.
        /// </summary>
        [Description("admin_logs")] AdminLogs,
    }

    public enum StandardEventType
    {
        /// <summary>
        ///   A folder or File was created
        /// </summary>
        ItemCreate,

        /// <summary>
        ///   A folder or File was uploaded
        /// </summary>
        ItemUpload,

        /// <summary>
        ///   A comment was created on a folder, file, discussion, or other comment
        /// </summary>
        CommentCreate,

        /// <summary>
        ///   A file or folder was downloaded
        /// </summary>
        ItemDownload,

        /// <summary>
        ///   A file was previewed
        /// </summary>
        ItemPreview,

        /// <summary>
        ///   A file or folder was moved
        /// </summary>
        ItemMove,

        /// <summary>
        ///   A file or folder was copied
        /// </summary>
        ItemCopy,

        /// <summary>
        ///   A task was assigned
        /// </summary>
        TaskAssignmentCreate,

        /// <summary>
        ///   A file was locked
        /// </summary>
        LockCreate,

        /// <summary>
        ///   A file was unlocked
        /// </summary>
        LockDestroy,

        /// <summary>
        ///   A file or folder was marked as deleted
        /// </summary>
        ItemTrash,

        /// <summary>
        ///   A file or folder was recovered out of the trash
        /// </summary>
        ItemUndeleteViaTrash,

        /// <summary>
        ///   A collaborator was added to a folder
        /// </summary>
        CollabAddCollaborator,

        /// <summary>
        ///   A collaborator was invited on a folder
        /// </summary>
        CollabInviteCollaborator,

        /// <summary>
        ///   A folder was marked for sync
        /// </summary>
        ItemSync,

        /// <summary>
        ///   A folder was un-marked for sync
        /// </summary>
        ItemUnsync,

        /// <summary>
        ///   A file or folder was renamed
        /// </summary>
        ItemRename,

        /// <summary>
        ///   A file or folder was enabled for sharing
        /// </summary>
        ItemSharedCreate,

        /// <summary>
        ///   A file or folder was disabled for sharing
        /// </summary>
        ItemSharedUnshare,

        /// <summary>
        ///   A folder was shared
        /// </summary>
        ItemShared,

        /// <summary>
        ///   A Tag was added to a file or folder
        /// </summary>
        TagItemCreate,
    }

    public enum EnterpriseEventType
    {
        GroupAddUser,
        NewUser,
        GroupCreation,
        GroupDeletion,
        DeleteUser,
        GroupEdited,
        EditUser,
        GroupAddFolder,
        GroupRemoveUser,
        GroupRemoveFolder,
        AddTrustedDevice,
        AdminLogin,
        AddDeviceAssociation,
        FailedLogin,
        Login,
        RemoveTrustedDevice,
        TermsOfServiceAgree,
        TermsOfServiceReject,
        Copy,
        Delete,
        Download,
        Edit,
        Lock,
        Move,
        Preview,
        Rename,
        StorageExpiration,
        Undelete,
        Unlock,
        Upload,
        Share,
        UpdateShareExpiration,
        ShareExpiration,
        Unshare,
        CollaborationAccept,
        CollaborationRoleChange,
        UpdateCollaborationExpiration,
        CollaborationRemove,
        CollaborationInvite,
        CollaborationExpiration,
        ItemSync,
        ItemUnsync
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