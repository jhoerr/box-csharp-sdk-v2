using System;
using System.Collections.Generic;

namespace BoxApi.V2.Model
{
    /// <summary>
    /// Provides information about a Box user
    /// </summary>
    public class User : UserEntity
    {
        /// <summary>
        ///   The time this item was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///   The time this item was modified
        /// </summary>
        public DateTime ModifiedAt { get; set; }

        /// <summary>
        ///   The total amount of space allocated to the account, in bytes
        /// </summary>
        public long SpaceAmount { get; set; }

        /// <summary>
        ///   The amount of space currently utilized, in bytes
        /// </summary>
        public long SpaceUsed { get; set; }

        /// <summary>
        ///   The maximum size of the file that user can upload, in bytes
        /// </summary>
        public long MaxUploadSize { get; set; }

        public string Role { get; set; }
        public string Language { get; set; }
        public List<string> TrackingCodes { get; set; }
        /// <summary>
        /// Whether this user can 
        /// </summary>
        public bool CanSeeManagedUsers { get; set; }
        public bool IsSyncEnabled { get; set; }
        public string Status { get; set; }
        public string JobTitle { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsExemptFromDeviceLimits { get; set; }
        public bool IsExemptFromLoginVerification { get; set; }
    }
}