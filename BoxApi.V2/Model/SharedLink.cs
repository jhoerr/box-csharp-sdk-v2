using System;
using BoxApi.V2.Model.Enum;
using Newtonsoft.Json;

namespace BoxApi.V2.Model
{
    /// <summary>
    ///     An object representing this item’s shared link and associated permissions
    /// </summary>
    public class SharedLink
    {
        /// <summary>
        ///     Parameterless constructor required for deserialization
        /// </summary>
        public SharedLink()
        {
        }

        /// <summary>
        ///     Creates a SharedLink to associate with a File or folder
        /// </summary>
        /// <param name="access">With whom the item should be shared</param>
        /// <param name="unsharedAt">Automatically stop sharing at this time</param>
        /// <param name="permissions">The </param>
        public SharedLink(Access access, DateTime? unsharedAt = null, Permissions permissions = null) : this()
        {
            Access = access;
            Permissions = permissions ?? new Permissions() {CanDownload = true, CanPreview = true};
            if (unsharedAt.HasValue)
            {
                UnsharedAt = unsharedAt.Value;
            }
        }

        /// <summary>
        ///     The level of access required for this shared link. Can be: Open, Company, Collaborators.
        /// </summary>
        public Access Access { get; set; }

        /// <summary>
        ///     The day that this link should be disabled at. Timestamps are rounded off to the given day.
        /// </summary>
        [JsonProperty(PropertyName = "unshared_at")]
        public DateTime? UnsharedAt { get; set; }

        /// <summary>
        ///     The set of permissions that apply to this shared item
        /// </summary>
        public Permissions Permissions { get; set; }

        /// <summary>
        ///     The URL to be used when requesting information about this item.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     The URL from which this item can be downloaded.
        /// </summary>
        public string DownloadUrl { get; set; }

        /// <summary>
        ///     Whether this item is password-protected
        /// </summary>
        [JsonProperty(PropertyName = "is_password_enabled")]
        public bool IsPasswordEnabled { get; set; }

        /// <summary>
        ///     The number of times this item has been downloaded
        /// </summary>
        public int DownloadCount { get; set; }

        /// <summary>
        ///     The number of times this item has been previewed
        /// </summary>
        public int PreviewCount { get; set; }
    }
}