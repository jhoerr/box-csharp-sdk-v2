namespace BoxApi.V2.Model.Fields
{
    public class EnterpriseUserField : UserField
    {
        public static Field Role = new Field("role");
        public static Field TrackingCodes = new Field("tracking_codes");
        public static Field CanSeeManagedUsers = new Field("can_see_managed_users");
        public static Field IsSyncEnabled = new Field("is_sync_enabled");
        public static Field IsExemptFromDeviceLimits = new Field("is_exempt_from_device_limits");
        public static Field IsExemptFromLoginVerification = new Field("is_exempt_from_login_verification");
        public static Field Enterprise = new Field("enterprise");
            
    }
}