namespace BoxApi.V2.Model.Fields
{
    public class DiscussionField : Field
    {
        public static DiscussionField Name = new DiscussionField("name");
        public static DiscussionField Parent = new DiscussionField("parent");
        public static DiscussionField Description = new DiscussionField("description");
        public static DiscussionField CreatedAt = new DiscussionField("created_at");
        public static DiscussionField ModifiedAt = new DiscussionField("modified_at");
        public static DiscussionField CreatedBy = new DiscussionField("created_by");

        public DiscussionField(string value) : base(value)
        {
        }
    }
}