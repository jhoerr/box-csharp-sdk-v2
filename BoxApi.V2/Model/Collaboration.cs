using System;
using BoxApi.V2.Model.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BoxApi.V2.Model
{
    /// <summary>
    /// Collaborations are Box’s equivalent of access control lists. They let you set and apply permissions for users to folders.
    /// You can read more about those permissions at https://support.box.com/entries/20366031-what-are-the-different-collaboration-permissions-and-what-access-do-they-provide.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Collaboration : ModifiableEntity
    {
        /// <summary>
        ///   The time this collaboration will expire
        /// </summary>
        [JsonProperty(PropertyName = "expires_at")]
        public DateTime? ExpiresAt { get; set; }
        
        /// <summary>
        /// The level of access this user has
        /// </summary>
        public CollaborationRole Role { get; set; }

        /// <summary>
        /// The folder this discussion is related to
        /// </summary>
        public Entity Item { get; set; }

        /// <summary>
        /// The 
        /// </summary>
        [JsonProperty(PropertyName = "accessible_by")]
        public UserEntity AccessibleBy { get; set; }

        [JsonProperty(PropertyName = "acknowledged_at")]
        public DateTime? AcknowledgedAt{ get; set; }

        public Status Status { get; set; }
    }
}