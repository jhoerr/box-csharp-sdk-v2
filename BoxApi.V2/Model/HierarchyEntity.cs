using Newtonsoft.Json;

namespace BoxApi.V2.Model
{
    /// <summary>
    ///     An item that exists as part of a hierarchy tree.
    /// </summary>
    public class HierarchyEntity : Version
    {
        /// <summary>
        ///     A unique ID for use with the /events endpoint
        /// </summary>
        public string SequenceId { get; set; }

        /// <summary>
        ///     The description of the item
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The folder that contains this item
        /// </summary>
        public MiniFolderEntity Parent { get; set; }

        /// <summary>
        /// The path of folders to this item, starting at the root
        /// </summary>
        [JsonProperty(PropertyName = "path_collection")]
        public PathCollection PathCollection { get; set; }

        /// <summary>
        ///     A unique string identifying the version of this file.
        /// </summary>
        public string Etag { get; set; }

        /// <summary>
        ///     If this file has been shared by another user
        /// </summary>
        [JsonProperty(PropertyName = "shared_link")]
        public SharedLink SharedLink { get; set; }
    }
}