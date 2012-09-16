using Newtonsoft.Json;

namespace BoxApi.V2.SDK.Model
{
    public class HierarchyEntity : ModifiableEntity
    {
        /// <summary>
        ///   The description of the item
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///   The item size in bytes
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        ///   The folder that contains this item
        /// </summary>
        public Entity Parent { get; set; }

        [JsonProperty(PropertyName = "shared_link")]
        public SharedLink SharedLink { get; set; }
    }
}