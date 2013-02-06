namespace BoxApi.V2.Model.Fields
{
    public class FileField : Field
    {
        public static FileField Sha1 = new FileField("sha1");
        public static FileField SequenceId = new FileField("sequence_id");
        public static FileField Etag = new FileField("etag");
        public static FileField Name = new FileField("name");
        public static FileField Description = new FileField("description");
        public static FileField Size = new FileField("size");
        public static FileField PathCollection = new FileField("path_collection");
        public static FileField ModifiedBy = new FileField("modified_by");
        public static FileField OwnedBy = new FileField("owned_by");
        public static FileField SharedLink = new FileField("shared_link");
        public static FileField Parent = new FileField("parent");
        public static FileField ItemStatus = new FileField("item_status");
        public static FileField CreatedAt = new FileField("created_at");
        public static FileField ModifiedAt = new FileField("modified_at");
        public static FileField CreatedBy = new FileField("created_by");

        public FileField(string value) : base(value)
        {
        }
    }
}