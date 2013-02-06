namespace BoxApi.V2.Model.Field
{
    public class CommentField : TemporalField
    {
        public static Field IsReplyComment = new Field("is_reply_comment");
        public static Field Message = new Field("message");
        public static Field Item = new Field("item");
    }
}