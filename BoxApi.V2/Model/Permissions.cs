using Newtonsoft.Json;

namespace BoxApi.V2.Model
{
    /// <summary>
    ///     The set of permissions that apply to a SharedLink
    /// </summary>
    public class Permissions
    {
        /// <summary>
        ///     Whether this link allows downloads
        /// </summary>
        [JsonProperty(PropertyName = "can_download")]
        public bool CanDownload { get; set; }

        /// <summary>
        ///     Whether this link allows previews
        /// </summary>
        [JsonProperty(PropertyName = "can_preview")]
        public bool CanPreview { get; set; }
    }
}