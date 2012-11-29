using System.ComponentModel;
using System.Runtime.Serialization;

namespace BoxApi.V2.Model.Enum
{
    /// <summary>
    /// Describes the current status of an Enterprise user
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// User is active within the Enterprise
        /// </summary>
        [EnumMember(Value = "active")] Active,
        /// <summary>
        /// User is not active within the Enterprise
        /// </summary>
        [EnumMember(Value = "inactive")] Inactive
    }
}