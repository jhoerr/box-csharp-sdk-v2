namespace BoxApi.V2.Model.Fields
{
    public sealed class CommentField : Field
    {
        public static CommentField IsReplyComment = new CommentField("is_reply_comment");
        public static CommentField Message = new CommentField("message");
        public static CommentField TaggedMessage = new CommentField("tagged_message");
        public static CommentField Item = new CommentField("item");
        public static CommentField CreatedAt = new CommentField("created_at");
        public static CommentField ModifiedAt = new CommentField("modified_at");
        public static CommentField CreatedBy = new CommentField("created_by");

        private CommentField(string value) : base(value)
        {
        }
    }
}