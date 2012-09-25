using System.Runtime.Serialization;

namespace BoxApi.V2.Model
{
    /// <summary>
    /// The set of permissions that apply to a SharedLink
    /// </summary>
    public class Permissions
    {
        /// <summary>
        /// Whether this link allows downloads
        /// </summary>
        public bool Download { get; set; }

        /// <summary>
        /// Whether this link allows previews
        /// </summary>
        public bool Preview { get; set; }
    }
}