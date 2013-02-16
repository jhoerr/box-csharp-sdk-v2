namespace BoxApi.V2.Model.Fields
{
    public sealed class FolderField : Field, IContentField
    {
        public static FolderField FolderUploadEmail = new FolderField("folder_upload_email");
        public static FolderField ItemCollection = new FolderField("item_collection");
        public static FolderField SyncState = new FolderField("sync_state");
        public static FolderField SequenceId = new FolderField("sequence_id");
        public static FolderField Etag = new FolderField("etag");
        public static FolderField Name = new FolderField("name");
        public static FolderField Description = new FolderField("description");
        public static FolderField Size = new FolderField("size");
        public static FolderField PathCollection = new FolderField("path_collection");
        public static FolderField ModifiedBy = new FolderField("modified_by");
        public static FolderField OwnedBy = new FolderField("owned_by");
        public static FolderField SharedLink = new FolderField("shared_link");
        public static FolderField Parent = new FolderField("parent");
        public static FolderField ItemStatus = new FolderField("item_status");
        public static FolderField CreatedAt = new FolderField("created_at");
        public static FolderField ModifiedAt = new FolderField("modified_at");
        public static FolderField CreatedBy = new FolderField("created_by");
        public static FolderField ContentCreatedAt = new FolderField("content_created_at");
        public static FolderField ContentModifiedAt = new FolderField("content_modified_at");

        private FolderField(string value) : base(value)
        {
        }
    }
}