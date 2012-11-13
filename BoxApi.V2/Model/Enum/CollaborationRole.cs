using System.ComponentModel;

namespace BoxApi.V2.Model.Enum
{
    public enum CollaborationRole
    {
        [Description("Editor")] Editor,
        [Description("Viewer")] Viewer,
        [Description("Previewer")] Previewer,
        [Description("Uploader")] Uploader,
        [Description("Previewer-Uploader")] PreviewerUploader,
        [Description("Viewer-Uploader")] ViewerUploader,
        [Description("Co-Owner")] CoOwner,
    }
}