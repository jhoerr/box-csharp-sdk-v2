namespace BoxApi.V2.Model
{
    public class TrackingCode //: EntityBase
    {
        public TrackingCode()
        {
            Type = "tracking_code";
        }

        public TrackingCode(string name, string value):this()
        {
            Name = name;
            Value = value;
        }

        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}