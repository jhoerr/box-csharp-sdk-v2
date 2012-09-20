namespace BoxApi.V2.SDK.Model
{
    /// <summary>
    ///   Represents Box.NET user entity
    /// </summary>
    public class User
    {
        /// <summary>
        /// The login/email address of the user
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        ///   The user's email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///   The unique ID of the user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///   If the user is a guest, the AccessID will be the ID of the guest's parent.
        ///   If this is a full user, the AccessID will be the same as the ID property
        /// </summary>
        public int AccessId { get; set; }

        /// <summary>
        ///   The total amount of space allocated to that account
        /// </summary>
        public long SpaceAmount { get; set; }

        /// <summary>
        ///   The amount of space currently utilized by the user
        /// </summary>
        public long SpaceUsed { get; set; }

        /// <summary>
        ///   The maximum size of the file that user can upload
        /// </summary>
        public long MaxUploadSize { get; set; }

        /// <summary>
        ///   Whether file sharing is disabled for the user
        /// </summary>
        public bool SharingDisabled { get; set; }
    }
}