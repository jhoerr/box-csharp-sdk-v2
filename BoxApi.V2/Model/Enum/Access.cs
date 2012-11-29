using System.Runtime.Serialization;

namespace BoxApi.V2.Model.Enum
{
    public enum Access
    {
        [EnumMember(Value = "open")]Open,
        [EnumMember(Value = "company")]Company,
        [EnumMember(Value = "collaborators")]Collaborators,
    }
}