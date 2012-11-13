using System.ComponentModel;

namespace BoxApi.V2.Model.Enum
{
    public enum Status
    {
        [Description("pending")] Pending,
        [Description("accepted")] Accepted,
        [Description("rejected")] Rejected,
    }
}