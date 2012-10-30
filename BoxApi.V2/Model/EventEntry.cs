using System;

namespace BoxApi.V2.Model
{
    /// <summary>
    /// Information on a specific event that occured.
    /// </summary>
    public class EventEntry : EntityBase
    {
        /// <summary>
        /// The id of the event, used for de-duplication purposes
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// The user that performed the action
        /// </summary>
        public UserEntity CreatedBy { get; set; }

        /// <summary>
        /// The time this item was created
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The nature of the event
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// The session of the user that performed the action
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// Information about the object that was modified.  This may be a subset of all properties that exist for a particular type of object.
        /// </summary>
        public ModifiableEntity Source { get; set; }
    }
}