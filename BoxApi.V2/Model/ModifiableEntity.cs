using System;

namespace BoxApi.V2.Model
{
    /// <summary>
    ///     An item that can be changed over time.
    /// </summary>
    public class ModifiableEntity : Entity
    {
        /// <summary>
        ///     The user who created this item
        /// </summary>
        public UserEntity CreatedBy { get; set; }

        /// <summary>
        ///     The time this item was created
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        ///     The time this item (or its contents) were last modified
        /// </summary>
        public UserEntity ModifedBy { get; set; }

        /// <summary>
        ///     The user who last modified this item
        /// </summary>
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        ///     The user who owns this item
        /// </summary>
        public UserEntity OwnedBy { get; set; }
    }
}