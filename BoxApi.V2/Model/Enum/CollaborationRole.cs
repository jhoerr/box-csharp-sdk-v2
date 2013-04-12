using System.ComponentModel;

namespace BoxApi.V2.Model.Enum
{
    /// <summary>
    ///     The roles that can be applied to a collaborating user
    /// </summary>
    public enum CollaborationRole
    {
        /// <summary>
        /// Undefined role.
        /// </summary>
        Undefined,

        /// <summary>
        ///     An Editor has full read/ write access to a folder. They can view and download
        ///     the contents of the folder, as well as upload new content into the folder. They
        ///     have permission to delete items, edit items, comment of files, generate a shared
        ///     link for items in the folder, and create tags. By default an Editor will be able
        ///     to invite new collaborators to a folder, however an editor cannot manage users
        ///     currently existing in the folder.
        /// </summary>
        [Description("editor")] Editor,

        /// <summary>
        ///     A Viewer has full read access to a folder. So they will be able to preview any
        ///     item using the integrated content viewer, and will be able to download any item in
        ///     the folder. A Viewer can generate a shared link for any item in the folder as well
        ///     as make comments on items. A viewer will not be able to add tags, invite new
        ///     collaborators, upload, or edit items in the folder.
        /// </summary>
        [Description("viewer")] Viewer,

        /// <summary>
        ///     A Previewer only has limited read access. This permission level allows a user to view
        ///     the items in the folder using the integrated content viewer or a viewing application
        ///     from the OpenBox directory such as Scribd. They will have no other access to the files
        ///     and will not be able to download, edit, or upload into the folder.
        /// </summary>
        /// <remarks>
        ///     Limited to Business and Enterprise accounts
        /// </remarks>
        [Description("previewer")] 
        Previewer,

        /// <summary>
        ///     An Uploader is the most limited access that a user can have in a folder and provides
        ///     limited write access. A user assigned uploader will see the items in a folder but will
        ///     not be able to download or view the items. The only action available will be to upload
        ///     content into the folder. If an Uploader uploads an item with the same name as an
        ///     existing item in the folder, the file will be updated and the existing version will be
        ///     moved into the version history.
        /// </summary>
        /// <remarks>
        ///     Limited to Business and Enterprise accounts
        /// </remarks>
        [Description("uploader")] Uploader,

        /// <summary>
        ///     This access level is a combination of Previewer and Uploader. A user with this access
        ///     level will be able to preview files in the folder using the integrated content viewer or
        ///     a viewing application from the OpenBox directory such as Scribd and will also be able to
        ///     upload items into the folder. If a Previewer-Uploader uploads an item with the same name
        ///     as an existing item in the folder, the file will be updated and the existing version will
        ///     be moved into the version history. They will have no other access to the files and will
        ///     not be able to download or edit items in the folder.
        /// </summary>
        /// <remarks>
        ///     Limited to Business and Enterprise accounts
        /// </remarks>
        [Description("previewer uploader")] PreviewerUploader,

        /// <summary>
        ///     This access level is a combination of Viewer and Uploader. A Viewer-Uploader has full
        ///     read access to a folder and limited write access. They will be able to preview any item
        ///     using the integrated content viewer, and will be able to download any item in the folder.
        ///     They can generate a shared link for any item in the folder as well as make comments on
        ///     items. A Viewer-Uploader will also be able to upload content into the folder. If a
        ///     Viewer-Uploader uploads an item with the same name as an existing item in the folder,
        ///     the file will be updated and the existing version will be moved into the version history.
        ///     They will not be able to add tags, invite new collaborators, or edit items in the folder.
        /// </summary>
        /// <remarks>
        ///     Limited to Business and Enterprise accounts
        /// </remarks>
        [Description("viewer uploader")] ViewerUploader,

        /// <summary>
        ///     A Co-Owner has all of the functional read/ write access that an Editor does. This
        ///     permission level has the added ability of being able to manage users in the folder.
        ///     A Co-Owner can add new collaborators, change collaborators access, and remove
        ///     collaborators (they will not be able to manipulate the owner of the folder or transfer
        ///     ownership to another user).
        /// </summary>
        /// <remarks>
        ///     Limited to Business and Enterprise accounts
        /// </remarks>
        [Description("co-owner")] CoOwner,
    }
}