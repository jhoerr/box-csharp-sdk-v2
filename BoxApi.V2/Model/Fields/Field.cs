namespace BoxApi.V2.Model.Fields
{
    public class Field
    {
        public Field(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}