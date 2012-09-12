using System.Runtime.Serialization;
using System.Text;

namespace BoxApi.V2.SDK.Model
{
    [DataContract]
    public class UserIdAndName
    {
        [DataMember]
        public int id;

        [DataMember]
        public string name;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("id: {0} name: {1}", id, name);
            return sb.ToString();
        }
    }
}