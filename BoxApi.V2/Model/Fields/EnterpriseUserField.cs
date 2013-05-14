namespace BoxApi.V2.Model.Fields
{
    public sealed class EnterpriseUserField : Field
    {
        public static EnterpriseUserField Address = new EnterpriseUserField("address");
        public static EnterpriseUserField AvatarUrl = new EnterpriseUserField("avatar_url");
        public static EnterpriseUserField CreatedAt = new EnterpriseUserField("created_at");
        public static EnterpriseUserField JobTitle = new EnterpriseUserField("job_title");
        public static EnterpriseUserField Language = new EnterpriseUserField("language");
        public static EnterpriseUserField Login = new EnterpriseUserField("login");
        public static EnterpriseUserField MaxUploadSize = new EnterpriseUserField("max_upload_size");
        public static EnterpriseUserField ModifiedAt = new EnterpriseUserField("modified_at");
        public static EnterpriseUserField Name = new EnterpriseUserField("name");
        public static EnterpriseUserField Phone = new EnterpriseUserField("phone");
        public static EnterpriseUserField SpaceAmount = new EnterpriseUserField("space_amount");
        public static EnterpriseUserField SpaceUsed = new EnterpriseUserField("space_used");
        public static EnterpriseUserField Status = new EnterpriseUserField("status");
        public static EnterpriseUserField Role = new EnterpriseUserField("role");
        public static EnterpriseUserField TrackingCodes = new EnterpriseUserField("tracking_codes");
        public static EnterpriseUserField CanSeeManagedUsers = new EnterpriseUserField("can_see_managed_users");
        public static EnterpriseUserField IsSyncEnabled = new EnterpriseUserField("is_sync_enabled");
        public static EnterpriseUserField IsExemptFromDeviceLimits = new EnterpriseUserField("is_exempt_from_device_limits");
        public static EnterpriseUserField IsExemptFromLoginVerification = new EnterpriseUserField("is_exempt_from_login_verification");
        public static EnterpriseUserField IsPasswordResetRequired = new EnterpriseUserField("is_password_reset_required");

        public static EnterpriseUserField[] All = new[]
            {
                Address, 
                AvatarUrl, 
                CreatedAt, 
                JobTitle, 
                Language, 
                Login, 
                MaxUploadSize, 
                ModifiedAt, 
                Name, 
                Phone, 
                SpaceAmount, 
                SpaceUsed, 
                Status,
                Role, 
                TrackingCodes, 
                CanSeeManagedUsers, 
                IsSyncEnabled, 
                IsExemptFromDeviceLimits, 
                IsExemptFromLoginVerification, 
                IsPasswordResetRequired, 
            };

        private EnterpriseUserField(string value) : base(value)
        {
        }
    }
}