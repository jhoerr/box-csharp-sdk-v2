using System.Runtime.Serialization;
using System.Text;

namespace BoxApi.V2.SDK.Model
{
    [DataContract]
    public class CommentingUser
    {
        [DataMember]
        public int id;

        [DataMember]
        public string name;

        [DataMember]
        public string small_thumb_api;
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("id: {0} name: {1} small_thumb_api:{2}", id, name, small_thumb_api);
            return sb.ToString();
        }
    }
}