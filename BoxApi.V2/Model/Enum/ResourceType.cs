using System.ComponentModel;

namespace BoxApi.V2.Model.Enum
{
    public enum ResourceType
    {
        [Description("unknown")]Unknown,
        [Description("files")]File,
        [Description("folders")] Folder,
        [Description("comments")] Comment,
        [Description("discussions")] Discussion,
        [Description("events")] Event,
        [Description("tokens")] Token,
        [Description("collaborations")] Collaboration,
        [Description("users")] User,
        [Description("shared_items")] SharedItem,
        [Description("error")]Error,
        [Description("email_alias")]EmailAlias,
        [Description("search")]Search,
        [Description("enterprise")]Enterprise,
    }
}