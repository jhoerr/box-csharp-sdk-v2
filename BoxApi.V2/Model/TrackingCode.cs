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

        protected bool Equals(TrackingCode other)
        {
            return string.Equals(Type, other.Type) && string.Equals(Name, other.Name) && string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TrackingCode) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Type != null ? Type.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Value != null ? Value.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}