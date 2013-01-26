using Newtonsoft.Json;

namespace BoxApi.V2.Model
{
    /// <summary>
    ///     A Box file.
    /// </summary>
    public class File : HierarchyEntity
    {
        /// <summary>
        /// The sha1 hash of this file
        /// </summary>
        public string Sha1 { get; set; }
    }
}