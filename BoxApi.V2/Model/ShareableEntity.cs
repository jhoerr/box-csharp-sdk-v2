using Newtonsoft.Json;

namespace BoxApi.V2.Model
{
    /// <summary>
    /// An entity that can be shared with another user.
    /// </summary>
    public class ShareableEntity : HierarchyEntity
    {
        /// <summary>
        /// If this file has been shared by another user
        /// </summary>
        [JsonProperty(PropertyName = "shared_link")]
        public SharedLink SharedLink { get; set; }

        /// <summary>
        ///   The item size in bytes
        /// </summary>
        public string Size { get; set; }
    }
}