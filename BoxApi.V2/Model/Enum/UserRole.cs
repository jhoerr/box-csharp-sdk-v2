using System.Runtime.Serialization;

namespace BoxApi.V2.Model.Enum
{
    public enum UserRole
    {
        [EnumMember(Value = "user")] User,
        [EnumMember(Value = "admin")] Admin,
        [EnumMember(Value = "coadmin")] CoAdmin,
    }
}