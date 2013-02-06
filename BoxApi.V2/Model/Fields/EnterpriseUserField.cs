namespace BoxApi.V2.Model.Fields
{
    public class EnterpriseUserField : UserField
    {
        public static UserField Role = new UserField("role");
        public static UserField TrackingCodes = new UserField("tracking_codes");
        public static UserField CanSeeManagedUsers = new UserField("can_see_managed_users");
        public static UserField IsSyncEnabled = new UserField("is_sync_enabled");
        public static UserField IsExemptFromDeviceLimits = new UserField("is_exempt_from_device_limits");
        public static UserField IsExemptFromLoginVerification = new UserField("is_exempt_from_login_verification");
        public static UserField Enterprise = new UserField("enterprise");

        public static UserField[] All = new UserField[]
            {
                Address, AvatarUrl, CreatedAt, JobTitle, Language, Login, MaxUploadSize, ModifiedAt, Name, Phone, SpaceAmount, SpaceUsed, Status,
                Role, TrackingCodes, CanSeeManagedUsers, IsSyncEnabled, IsExemptFromDeviceLimits, IsExemptFromLoginVerification, Enterprise
            };

        public EnterpriseUserField(string value) : base(value)
        {
        }
    }
}