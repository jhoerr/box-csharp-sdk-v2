namespace BoxApi.V2.Model.Fields
{
    public sealed class CollaborationField : Field
    {
        public static CollaborationField Status = new CollaborationField("status");
        public static CollaborationField AccessibleBy = new CollaborationField("accessible_by");
        public static CollaborationField Role = new CollaborationField("role");
        public static CollaborationField AcknowledgedAt = new CollaborationField("acknowledged_at");
        public static CollaborationField Item = new CollaborationField("item");
        public static CollaborationField CreatedAt = new CollaborationField("created_at");
        public static CollaborationField ModifiedAt = new CollaborationField("modified_at");
        public static CollaborationField CreatedBy = new CollaborationField("created_by");
        public static CollaborationField ExpiresAt = new CollaborationField("expires_at");

        private CollaborationField(string value) : base(value)
        {
        }
    }
}