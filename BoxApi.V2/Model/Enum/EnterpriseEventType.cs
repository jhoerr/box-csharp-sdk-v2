namespace BoxApi.V2.Model.Enum
{
    /// <summary>
    /// Events that are tracked at the enterprise level
    /// </summary>
    public enum EnterpriseEventType
    {
        /// <summary>
        /// Added user to group
        /// </summary>
        GroupAddUser,
        /// <summary>
        /// Created user
        /// </summary>
        NewUser,
        /// <summary>
        /// Created new group
        /// </summary>
        GroupCreation,
        /// <summary>
        /// Deleted group
        /// </summary>
        GroupDeletion,
        /// <summary>
        /// Deleted user
        /// </summary>
        DeleteUser,
        /// <summary>
        /// Edited group
        /// </summary>
        GroupEdited,
        /// <summary>
        /// Edited user
        /// </summary>
        EditUser,
        /// <summary>
        /// Granted folder access
        /// </summary>
        GroupAddFolder,
        /// <summary>
        /// Removed user from group
        /// </summary>
        GroupRemoveUser,
        /// <summary>
        /// Removed folder access from group
        /// </summary>
        GroupRemoveFolder,
        /// <summary>
        /// Added trusted application
        /// </summary>
        AddTrustedDevice,
        /// <summary>
        /// Administrative login
        /// </summary>
        AdminLogin,
        /// <summary>
        /// Added device assocation
        /// </summary>
        AddDeviceAssociation,
        /// <summary>
        /// Failed login
        /// </summary>
        FailedLogin,
        /// <summary>
        /// Logged in
        /// </summary>
        Login,
        /// <summary>
        /// Removed trusted application
        /// </summary>
        RemoveTrustedDevice,
        /// <summary>
        /// Removed device association
        /// </summary>
        RemoveDeviceAssociation,
        /// <summary>
        /// Agreed to terms of service
        /// </summary>
        TermsOfServiceAgree,
        /// <summary>
        /// Rejected terms of service
        /// </summary>
        TermsOfServiceReject,
        /// <summary>
        /// Item copied
        /// </summary>
        Copy,
        /// <summary>
        /// Item deleted
        /// </summary>
        Delete,
        /// <summary>
        /// Item downloaded
        /// </summary>
        Download,
        /// <summary>
        /// Item edited
        /// </summary>
        Edit,
        /// <summary>
        /// Item locked
        /// </summary>
        Lock,
        /// <summary>
        /// Item moved
        /// </summary>
        Move,
        /// <summary>
        /// Item previewed
        /// </summary>
        Preview,
        /// <summary>
        /// Item renamed
        /// </summary>
        Rename,
        /// <summary>
        /// Set file auto-delete
        /// </summary>
        StorageExpiration,
        /// <summary>
        /// Item undeleted
        /// </summary>
        Undelete,
        /// <summary>
        /// Item unlocked
        /// </summary>
        Unlock,
        /// <summary>
        /// Item uploaded
        /// </summary>
        Upload,
        /// <summary>
        /// Item shared
        /// </summary>
        Share,
        /// <summary>
        /// Extend shared link expiration
        /// </summary>
        UpdateShareExpiration,
        /// <summary>
        /// Set shared link expiration
        /// </summary>
        ShareExpiration,
        /// <summary>
        /// Unshared links
        /// </summary>
        Unshare,
        /// <summary>
        /// Accepted collaboration invitation
        /// </summary>
        CollaborationAccept,
        /// <summary>
        /// Changed user's collaboration role
        /// </summary>
        CollaborationRoleChange,
        /// <summary>
        /// Extended collaboration expiration 
        /// </summary>
        UpdateCollaborationExpiration,
        /// <summary>
        /// Removed collaborators
        /// </summary>
        CollaborationRemove,
        /// <summary>
        /// Invited collaborators
        /// </summary>
        CollaborationInvite,
        /// <summary>
        /// Set collaboration expiration
        /// </summary>
        CollaborationExpiration,
        /// <summary>
        /// Synced folder
        /// </summary>
        ItemSync,
        /// <summary>
        /// Unsynced folder
        /// </summary>
        ItemUnsync
    }
}