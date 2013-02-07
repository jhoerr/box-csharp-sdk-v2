namespace BoxApi.V2.Model.Fields
{
    public sealed class UserField : Field
    {
        public static UserField Address = new UserField("address");
        public static UserField AvatarUrl = new UserField("avatar_url");
        public static UserField CreatedAt = new UserField("created_at");
        public static UserField JobTitle = new UserField("job_title");
        public static UserField Language = new UserField("language");
        public static UserField Login = new UserField("login");
        public static UserField MaxUploadSize = new UserField("max_upload_size");
        public static UserField ModifiedAt = new UserField("modified_at");
        public static UserField Name = new UserField("name");
        public static UserField Phone = new UserField("phone");
        public static UserField SpaceAmount = new UserField("space_amount");
        public static UserField SpaceUsed = new UserField("space_used");
        public static UserField Status = new UserField("status");

        private UserField(string value) : base(value)
        {
        }
    }
}