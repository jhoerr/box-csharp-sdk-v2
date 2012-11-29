using System.Collections.Generic;
using BoxApi.V2.Model.Enum;

namespace BoxApi.V2.Model
{
    /// <summary>
    /// The set of User properties that can be managed by an administrator
    /// </summary>
    public class ManagedUser : UserEntity
    {
        /// <summary>
        ///     The user’s total available space amount in bytes
        /// </summary>
        public long SpaceAmount { get; set; }

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
        public Dictionary<string, string> TrackingCodes { get; set; }

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
        /// Whether to exempt this user from Enterprise device limits
        /// </summary>
        public bool IsExemptFromDeviceLimits { get; set; }

        /// <summary>
        /// Whether or not this user must use two-factor authentication
        /// </summary>
        public bool IsExemptFromLoginVerification { get; set; }
    }
}