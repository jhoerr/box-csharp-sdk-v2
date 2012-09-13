using System.Text;

namespace BoxApi.V2.SDK.Model
{
    public class Entity
    {
        public string Type { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("type: {0} id: {1} name: {2}", Type, Id, Name);
            return sb.ToString();
        }
    }
}