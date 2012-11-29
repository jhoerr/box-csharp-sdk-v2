using System;
using System.Collections.Generic;
using BoxApi.V2.Model.Enum;

namespace BoxApi.V2.Model
{
    /// <summary>
    ///     Provides information about a Box user
    /// </summary>
    public class User : ManagedUser
    {
        /// <summary>
        ///     The time this user was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     The time this user was last modified
        /// </summary>
        public DateTime ModifiedAt { get; set; }

        /// <summary>
        ///     The amount of space in use by the user in bytes
        /// </summary>
        public long SpaceUsed { get; set; }

        /// <summary>
        ///     The maximum individual file size in bytes this user can have
        /// </summary>
        public long MaxUploadSize { get; set; }

        /// <summary>
        ///     The URL for the user's avatar image
        /// </summary>
        public string AvatarUrl { get; set; }
    }
}