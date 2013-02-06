namespace BoxApi.V2.Model.Field
{
    public class ContentFieldBase : TemporalField, IContentField
    {
        public static Field SequenceId = new Field("sequence_id");
        public static Field Etag = new Field("etag");
        public static Field Name = new Field("name");
        public static Field Description = new Field("description");
        public static Field Size = new Field("size");
        public static Field PathCollection = new Field("path_collection");
        public static Field ModifiedBy = new Field("modified_by");
        public static Field OwnedBy = new Field("owned_by");
        public static Field SharedLink = new Field("shared_link");
        public static Field Parent = new Field("parent");
        public static Field ItemStatus = new Field("item_status");
    }
}