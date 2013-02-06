using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxApi.V2.Model.Field
{
    public class CollaborationField : TemporalField
    {
        public static Field ExpiresAt = new Field("expires_at");
        public static Field Status = new Field("status");
        public static Field AccessibleBy = new Field("accessible_by");
        public static Field Role = new Field("role");
        public static Field AcknowledgedAt = new Field("acknowledged_at");
        public static Field Item = new Field("item");
    }
}
