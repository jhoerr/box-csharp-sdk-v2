using System.Runtime.Serialization;
using System.Text;

namespace BoxApi.V2.SDK.Model
{
    [DataContract]
    public class Item
    {
        [DataMember]
        public string type;

        [DataMember]
        public int id;

        [DataMember]
        public string name;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("type: {0} id: {1} name: {2}", type, id, name);
            return sb.ToString();
        }
    }
}