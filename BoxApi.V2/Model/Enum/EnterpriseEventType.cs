using System.ComponentModel;

namespace BoxApi.V2.Model.Enum
{
    /// <summary>
    ///     Events that are tracked at the enterprise level
    /// </summary>
    public enum EnterpriseEventType
    {
        /// <summary>
        ///     Added user to group
        /// </summary>
        [Description("GROUP_ADD_USER")]
        GroupAddUser,

        /// <summary>
        ///     Created user
        /// </summary>
        [Description("NEW_USER")]
        NewUser,

        /// <summary>
        ///     Created new group
        /// </summary>
        [Description("GROUP_CREATION")]
        GroupCreation,

        /// <summary>
        ///     Deleted group
        /// </summary>
        [Description("GROUP_DELETION")]
        GroupDeletion,

        /// <summary>
        ///     Deleted user
        /// </summary>
        [Description("DELETE_USER")]
        DeleteUser,

        /// <summary>
        ///     Edited group
        /// </summary>
        [Description("GROUP_EDITED")]
        GroupEdited,

        /// <summary>
        ///     Edited user
        /// </summary>
        [Description("EDIT_USER")]
        EditUser,

        /// <summary>
        ///     Granted folder access
        /// </summary>
        [Description("GROUP_ADD_FOLDER")]
        GroupAddFolder,

        /// <summary>
        ///     Removed user from group
        /// </summary>
        [Description("GROUP_REMOVE_USER")]
        GroupRemoveUser,

        /// <summary>
        ///     Removed folder access from group
        /// </summary>
        [Description("GROUP_REMOVE_FOLDER")]
        GroupRemoveFolder,

        /// <summary>
        ///     Added trusted application
        /// </summary>
        [Description("ADD_TRUSTED_DEVICE")]
        AddTrustedDevice,

        /// <summary>
        ///     Administrative login
        /// </summary>
        [Description("ADMIN_LOGIN")]
        AdminLogin,

        /// <summary>
        ///     Added device assocation
        /// </summary>
        [Description("GROUP_EDITED")]
        AddDeviceAssociation,

        /// <summary>
        ///     Failed login
        /// </summary>
        [Description("FAILED_LOGIN")]
        FailedLogin,

        /// <summary>
        ///     Logged in
        /// </summary>
        [Description("LOGIN")]
        Login,

        /// <summary>
        ///     Removed trusted application
        /// </summary>
        [Description("REMOVE_TRUSTED_DEVICE")]
        RemoveTrustedDevice,

        /// <summary>
        ///     Removed device association
        /// </summary>
        [Description("REMOVE_DEVICE_ASSOCIATION")]
        RemoveDeviceAssociation,

        /// <summary>
        ///     Agreed to terms of service
        /// </summary>
        [Description("TERMS_OF_SERVICE_AGREE")]
        TermsOfServiceAgree,

        /// <summary>
        ///     Rejected terms of service
        /// </summary>
        [Description("TERMS_OF_SERVICE_REJECT")]
        TermsOfServiceReject,

        /// <summary>
        ///     Item copied
        /// </summary>
        [Description("COPY")]
        Copy,

        /// <summary>
        ///     Item deleted
        /// </summary>
        [Description("DELETE")]
        Delete,

        /// <summary>
        ///     Item downloaded
        /// </summary>
        [Description("DOWNLOAD")]
        Download,

        /// <summary>
        ///     Item edited
        /// </summary>
        [Description("EDIT")]
        Edit,

        /// <summary>
        ///     Item locked
        /// </summary>
        [Description("LOCK")]
        Lock,

        /// <summary>
        ///     Item moved
        /// </summary>
        [Description("MOVE")]
        Move,

        /// <summary>
        ///     Item previewed
        /// </summary>
        [Description("PREVIEW")]
        Preview,

        /// <summary>
        ///     Item renamed
        /// </summary>
        [Description("RENAME")]
        Rename,

        /// <summary>
        ///     Set file auto-delete
        /// </summary>
        [Description("STORAGE_EXPIRATION")]
        StorageExpiration,

        /// <summary>
        ///     Item undeleted
        /// </summary>
        [Description("UNDELETE")]
        Undelete,

        /// <summary>
        ///     Item unlocked
        /// </summary>
        [Description("UNLOCK")]
        Unlock,

        /// <summary>
        ///     Item uploaded
        /// </summary>
        [Description("UPLOAD")]
        Upload,

        /// <summary>
        ///     Item shared
        /// </summary>
        [Description("SHARE")]
        Share,

        /// <summary>
        ///     Extend shared link expiration
        /// </summary>
        [Description("UPDATE_SHARE_EXPIRATION")]
        UpdateShareExpiration,

        /// <summary>
        ///     Set shared link expiration
        /// </summary>
        [Description("SHARE_EXPIRATION")]
        ShareExpiration,

        /// <summary>
        ///     Unshared links
        /// </summary>
        [Description("UNSHARE")]
        Unshare,

        /// <summary>
        ///     Accepted collaboration invitation
        /// </summary>
        [Description("COLLABORATION_ACCEPT")]
        CollaborationAccept,

        /// <summary>
        ///     Changed user's collaboration role
        /// </summary>
        [Description("COLLABORATION_ROLE_CHANGE")]
        CollaborationRoleChange,

        /// <summary>
        ///     Extended collaboration expiration
        /// </summary>
        [Description("UPDATE_COLLABORATION_EXPIRATION")]
        UpdateCollaborationExpiration,

        /// <summary>
        ///     Removed collaborators
        /// </summary>
        [Description("COLLABORATION_REMOVE")]
        CollaborationRemove,

        /// <summary>
        ///     Invited collaborators
        /// </summary>
        [Description("COLLABORATION_INVITE")]
        CollaborationInvite,

        /// <summary>
        ///     Set collaboration expiration
        /// </summary>
        [Description("COLLABORATION_EXPIRATION")]
        CollaborationExpiration,

        /// <summary>
        ///     Synced folder
        /// </summary>
        [Description("ITEM_SYNC")]
        ItemSync,

        /// <summary>
        ///     Unsynced folder
        /// </summary>
        [Description("ITEM_UNSYNC")]
        ItemUnsync
    }
}