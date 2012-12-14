using System.Collections.Generic;
using BoxApi.V2.Model.Enum;
using Newtonsoft.Json;

namespace BoxApi.V2.Model
{
    /// <summary>
    /// The set of User properties that can be managed by an administrator
    /// </summary>
    public class ManagedUser : UserEntity
    {
        public ManagedUser()
        {
            TrackingCodes = new List<KeyValuePair<string, string>>();
        }

        /// <summary>
        ///     The user’s total available space amount in bytes
        /// </summary>
        [JsonProperty(PropertyName = "space_amount")]
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
        [JsonProperty(PropertyName = "tracking_codes")]
        public List<KeyValuePair<string, string>> TrackingCodes { get; set; }

        /// <summary>
        ///     Whether this user can see other enterprise users in its contact list
        /// </summary>
        [JsonProperty(PropertyName = "can_see_managed_users")]
        public bool CanSeeManagedUsers { get; set; }

        /// <summary>
        ///     Whether or not this user can use Box Sync
        /// </summary>
        [JsonProperty(PropertyName = "is_sync_enabled")]
        public bool IsSyncEnabled { get; set; }

        /// <summary>
        ///     The status of the user's account
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        ///     The user's job title
        /// </summary>
        [JsonProperty(PropertyName = "job_title")]
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
        [JsonProperty(PropertyName = "is_exempt_from_device_limits")]
        public bool IsExemptFromDeviceLimits { get; set; }

        /// <summary>
        /// Whether or not this user must use two-factor authentication
        /// </summary>
        [JsonProperty(PropertyName = "is_exempt_from_login_verification")]
        public bool IsExemptFromLoginVerification { get; set; }

        public object ToUpdateRequestBody()
        {
            return new
                {
                    name = Name,
                    role = Role,
                    language = Language,
                    is_sync_enabled = IsSyncEnabled,
                    job_title = JobTitle,
                    phone = Phone,
                    address = Address,
                    space_amount = SpaceAmount,
                    tracking_codes = TrackingCodes,
                    can_see_managed_users = CanSeeManagedUsers,
                    status = Status,
                    is_exempt_from_device_limits = IsExemptFromDeviceLimits,
                    is_exempt_from_login_verification = IsExemptFromLoginVerification,
                };
        }
    }
}