using Newtonsoft.Json;

namespace BoxApi.V2.Model
{
    public class ShareableEntity : HierarchyEntity
    {
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class File : ShareableEntity
    {
        /// <summary>
        ///   The item size in bytes
        /// </summary>
        public string Size { get; set; }

        [JsonProperty(PropertyName = "shared_link")]
        public SharedLink SharedLink { get; set; }

        /// <summary>
        ///   The path from the user’s root to this file by folder names
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        ///   The path from the root to this file by folder ids
        /// </summary>
        [JsonProperty(PropertyName = "path_id")]
        public string PathId { get; set; }

        /// <summary>
        ///   The sha1 hash of the file.  Useful for quickly determining if the contents of the file have changed.
        /// </summary>
        public string Etag { get; set; }
    }
}