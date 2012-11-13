using System;
using BoxApi.V2.Model.Enum;

namespace BoxApi.V2.Model
{
    /// <summary>
    ///     Information on a specific event that occured.
    /// </summary>
    public class Event : EntityBase
    {
        /// <summary>
        ///     The id of the event, used for de-duplication purposes
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        ///     The user that performed the action
        /// </summary>
        public UserEntity CreatedBy { get; set; }

        /// <summary>
        ///     The time this item was created
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        ///     The session of the user that performed the action
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        ///     Information about the object that was modified.  This may be a subset of all properties that exist for a particular type of object.
        /// </summary>
        public ModifiableEntity Source { get; set; }
    }

    /// <summary>
    ///     An event that occured in a user's account
    /// </summary>
    public class UserEvent : Event
    {
        /// <summary>
        ///     The nature of the event
        /// </summary>
        public StandardEventType EventType { get; set; }
    }

    /// <summary>
    ///     An event that occured in an enterprise
    /// </summary>
    public class EnterpriseEvent : Event
    {
        /// <summary>
        ///     The nature of the event
        /// </summary>
        public EnterpriseEventType EventType { get; set; }
    }
}