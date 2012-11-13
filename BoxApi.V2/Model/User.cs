using System;
using System.Collections.Generic;
using BoxApi.V2.Model.Enum;

namespace BoxApi.V2.Model
{
    /// <summary>
    ///     Provides information about a Box user
    /// </summary>
    public class User : UserEntity
    {
        /// <summary>
        ///     The time this item was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     The time this item was modified
        /// </summary>
        public DateTime ModifiedAt { get; set; }

        /// <summary>
        ///     The total amount of space allocated to the account, in bytes
        /// </summary>
        public long SpaceAmount { get; set; }

        /// <summary>
        ///     The amount of space currently utilized, in bytes
        /// </summary>
        public long SpaceUsed { get; set; }

        /// <summary>
        ///     The maximum size of the file that user can upload, in bytes
        /// </summary>
        public long MaxUploadSize { get; set; }

        /// <summary>
        ///     If the user is part of an enterprise, then this will reflect whether they are a regular user or an admin.
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        ///     The user's preferred language
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        ///     A collection of tracking codes currently applied to this user.  (?)
        /// </summary>
        public List<string> TrackingCodes { get; set; }

        /// <summary>
        ///     Whether this user can view information about other users in an enterprise.
        /// </summary>
        public bool CanSeeManagedUsers { get; set; }

        /// <summary>
        ///     Whether this user can use Box sync functionality
        /// </summary>
        public bool IsSyncEnabled { get; set; }

        /// <summary>
        ///     The status of the user's account
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     The user's job title
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        ///     The user's phone number
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     The user's street address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     The URL for the user's avatar image
        /// </summary>
        public string AvatarUrl { get; set; }

        public bool IsExemptFromDeviceLimits { get; set; }
        public bool IsExemptFromLoginVerification { get; set; }
    }
}