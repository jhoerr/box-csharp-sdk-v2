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
        ///     The time this user was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     The time this user was last modified
        /// </summary>
        public DateTime ModifiedAt { get; set; }

        /// <summary>
        ///     The user’s total available space amount in bytes
        /// </summary>
        public long SpaceAmount { get; set; }

        /// <summary>
        ///     The amount of space in use by the user in bytes
        /// </summary>
        public long SpaceUsed { get; set; }

        /// <summary>
        ///     The maximum individual file size in bytes this user can have
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
        ///     An array of key/value pairs set by the user’s admin
        /// </summary>
        public List<string> TrackingCodes { get; set; }

        /// <summary>
        ///     Whether this user can see other enterprise users in its contact list
        /// </summary>
        public bool CanSeeManagedUsers { get; set; }

        /// <summary>
        ///     Whether or not this user can use Box Sync
        /// </summary>
        public bool IsSyncEnabled { get; set; }

        /// <summary>
        ///     The status of the user's account
        /// </summary>
        public UserStatus Status { get; set; }

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

        /// <summary>
        /// Whether to exempt this user from Enterprise device limits
        /// </summary>
        public bool IsExemptFromDeviceLimits { get; set; }

        /// <summary>
        /// Whether or not this user must use two-factor authentication
        /// </summary>
        public bool IsExemptFromLoginVerification { get; set; }
    }
}