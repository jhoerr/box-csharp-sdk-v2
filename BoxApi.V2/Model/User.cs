using System;
using BoxApi.V2.Model.Enum;
using Newtonsoft.Json;

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
        [JsonProperty(PropertyName = "created_at")]
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     The time this user was last modified
        /// </summary>
        [JsonProperty(PropertyName = "modified_at")]
        [JsonIgnore]
        public DateTime ModifiedAt { get; set; }

        /// <summary>
        ///     The amount of space in use by the user in bytes
        /// </summary>
        [JsonProperty(PropertyName = "space_used")]
        [JsonIgnore]
        public double SpaceUsed { get; set; }

        /// <summary>
        ///     The maximum individual file size in bytes this user can have
        /// </summary>
        [JsonProperty(PropertyName = "max_upload_size")]
        [JsonIgnore]
        public double MaxUploadSize { get; set; }

        /// <summary>
        ///     The URL for the user's avatar image
        /// </summary>
        [JsonProperty(PropertyName = "avatar_url")]
        [JsonIgnore]
        public string AvatarUrl { get; set; }

        /// <summary>
        ///     The user’s total available space amount in bytes
        /// </summary>
        [JsonProperty(PropertyName = "space_amount")]
        public double SpaceAmount { get; set; }

        /// <summary>
        ///     The user's preferred language
        /// </summary>
        public string Language { get; set; }

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
    }
}