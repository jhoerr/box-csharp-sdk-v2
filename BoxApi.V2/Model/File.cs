using Newtonsoft.Json;

namespace BoxApi.V2.Model
{
    /// <summary>
    ///     A Box file.
    /// </summary>
    public class File : HierarchyEntity
    {
        /// <summary>
        ///     The sha1 hash of the file.  Useful for quickly determining if the contents of the file have changed.
        /// </summary>
        public string Etag { get; set; }

        /// <summary>
        ///     If this file has been shared by another user
        /// </summary>
        [JsonProperty(PropertyName = "shared_link")]
        public SharedLink SharedLink { get; set; }
    }
}