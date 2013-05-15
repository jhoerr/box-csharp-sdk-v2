using System;
using Newtonsoft.Json;

namespace BoxApi.V2.Model
{
    /// <summary>
    ///     A Box file.
    /// </summary>
    public class File : HierarchyEntity
    {
        /// <summary>
        ///     The sha1 hash of this file
        /// </summary>
        public string Sha1 { get; set; }

        /// <summary>
        ///     When the content of this file was created
        /// </summary>
        [JsonProperty(PropertyName = "content_created_at")]
        public DateTime? ContentCreatedAt { get; set; }

        /// <summary>
        ///     When the content of this file was last modified
        /// </summary>
        [JsonProperty(PropertyName = "content_modified_at")]
        public DateTime? ContentModifiedAt { get; set; }
    }
}