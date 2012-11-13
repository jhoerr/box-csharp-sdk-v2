namespace BoxApi.V2.Model.Enum
{
    /// <summary>
    /// Events that are tracked at the user level
    /// </summary>
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
}