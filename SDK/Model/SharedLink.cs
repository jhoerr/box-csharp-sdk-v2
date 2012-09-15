using System;
using Newtonsoft.Json;

namespace BoxApi.V2.SDK.Model
{
    /// <summary>
    ///   An object representing this item’s shared link and associated permissions
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class SharedLink
    {
        public SharedLink()
        {
        }

        public SharedLink(Access access, DateTime unsharedAt, Permissions permissions)
        {
            Access = access.Name();
            UnsharedAt = new DateTime(unsharedAt.Year, unsharedAt.Month, unsharedAt.Day, unsharedAt.Hour, unsharedAt.Minute, unsharedAt.Second);
            Permissions = permissions;
        }

        /// <summary>
        ///   The level of access required for this shared link. Can be: Open, Company, Collaborators.
        /// </summary>
        public string Access { get; set; }

        /// <summary>
        ///   The day that this link should be disabled at. Timestamps are rounded off to the given day.
        /// </summary>
        [JsonProperty(PropertyName = "unshared_at")]
        public DateTime UnsharedAt { get; set; }

        /// <summary>
        ///   The set of permissions that apply to this link
        /// </summary>
        public Permissions Permissions { get; set; }

        public string Url { get; set; }

        [JsonProperty(PropertyName = "download_url")]
        public string DownloadUrl { get; set; }

        [JsonProperty(PropertyName = "password_enabled")]
        public bool PasswordEnabled { get; set; }

        [JsonProperty(PropertyName = "download_count")]
        public int DownloadCount { get; set; }
        
        [JsonProperty(PropertyName = "preview_count")]
        public int PreviewCount { get; set; }
    }
}