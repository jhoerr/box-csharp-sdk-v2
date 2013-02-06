namespace BoxApi.V2.Model.Fields
{
    public class DiscussionField : TemporalField
    {
        public static Field Name = new Field("name");
        public static Field Parent = new Field("parent");
        public static Field Description = new Field("description");
    }
}