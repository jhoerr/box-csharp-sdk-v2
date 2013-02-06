namespace BoxApi.V2.Model.Field
{
    public class Field
    {
        public string Value { get; private set; }

        public Field(string value)
        {
            Value = value;
        }
    }
}