using Newtonsoft.Json;

namespace BoxApi.V2.SDK.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class File : HierarchyEntity
    {
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