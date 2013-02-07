using System.Collections.Generic;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Model.Fields;
using Newtonsoft.Json;

namespace BoxApi.V2.Model
{
    /// <summary>
    ///     Provides information about a Box user that is a member of an enterprise
    /// </summary>
    public class EnterpriseUser : User
    {
        /// <summary>
        /// The collection of fields that define all available properties for an Enterprise user.
        /// </summary>
        public static readonly IField[] Fields = new IField[]
            {
                UserField.Name,UserField.Login,UserField.CreatedAt,UserField.ModifiedAt,EnterpriseUserField.Role, 
               UserField.Language,UserField.SpaceAmount,UserField.SpaceUsed,UserField.MaxUploadSize, 
               EnterpriseUserField.TrackingCodes,EnterpriseUserField.CanSeeManagedUsers,EnterpriseUserField.IsSyncEnabled,
               UserField.Status,UserField.JobTitle,UserField.Phone,UserField.Address,UserField.AvatarUrl, 
               EnterpriseUserField.IsExemptFromDeviceLimits,EnterpriseUserField.IsExemptFromLoginVerification
            };

        /// <summary>
        ///     If the user is part of an enterprise, then this will reflect whether they are a regular user or an admin.
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        ///     An array of key/value pairs set by the user’s admin
        /// </summary>
        [JsonProperty(PropertyName = "tracking_codes")]
        public List<TrackingCode> TrackingCodes { get; set; }

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
        ///     Whether to exempt this user from Enterprise device limits
        /// </summary>
        [JsonProperty(PropertyName = "is_exempt_from_device_limits")]
        public bool IsExemptFromDeviceLimits { get; set; }

        /// <summary>
        ///     Whether or not this user must use two-factor authentication
        /// </summary>
        [JsonProperty(PropertyName = "is_exempt_from_login_verification")]
        public bool IsExemptFromLoginVerification { get; set; }
    }
}