using System;
using System.Collections.Generic;
using BoxApi.V2.Model.Enum;
using Newtonsoft.Json;

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
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     The time this user was last modified
        /// </summary>
        [JsonProperty(PropertyName = "modified_at")]
        public DateTime ModifiedAt { get; set; }

        /// <summary>
        ///     The amount of space in use by the user in bytes
        /// </summary>
        [JsonProperty(PropertyName = "space_used")]
        public long SpaceUsed { get; set; }

        /// <summary>
        ///     The maximum individual file size in bytes this user can have
        /// </summary>
        [JsonProperty(PropertyName = "max_upload_size")]
        public long MaxUploadSize { get; set; }

        /// <summary>
        ///     The URL for the user's avatar image
        /// </summary>
        [JsonProperty(PropertyName = "avatar_url")]
        public string AvatarUrl { get; set; }
    }
}