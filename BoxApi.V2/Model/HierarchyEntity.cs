namespace BoxApi.V2.Model
{
    /// <summary>
    ///     An item that exists as part of a hierarchy tree.
    /// </summary>
    public class HierarchyEntity : ModifiableEntity
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
        public Entity Parent { get; set; }
    }
}