using System;
using BoxApi.V2.Model.Enum;
using Newtonsoft.Json;

namespace BoxApi.V2.Model
{
    /// <summary>
    ///     Collaborations are Box’s equivalent of access control lists. They let you set and apply permissions for users to folders.
    /// </summary>
    public class Collaboration : ModifiableEntity
    {
        /// <summary>
        ///     The time this collaboration will automatically expire
        /// </summary>
        [JsonProperty(PropertyName = "expires_at")]
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        ///     The level of access this user has to the folder.
        /// </summary>
        public CollaborationRole Role { get; set; }

        /// <summary>
        ///     The folder in which this collaboration is taking place
        /// </summary>
        public Entity Item { get; set; }

        /// <summary>
        ///     The user with whom the collaboration exists
        /// </summary>
        [JsonProperty(PropertyName = "accessible_by")]
        public UserEntity AccessibleBy { get; set; }

        /// <summary>
        ///     When the collaborating user acknowledged their role and status
        /// </summary>
        [JsonProperty(PropertyName = "acknowledged_at")]
        public DateTime? AcknowledgedAt { get; set; }

        /// <summary>
        ///     Whether the user has Accepted or Rejected their role in the collaboration
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        ///     A unique ID for use with the /events endpoint
        /// </summary>
        public string SequenceId { get; set; }
    }
}