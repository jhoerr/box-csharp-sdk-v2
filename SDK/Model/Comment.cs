using System.Runtime.Serialization;
using System.Text;

namespace BoxApi.V2.SDK.Model
{
    [DataContract]
    public class Comment
    {
        [DataMember]
        public string type;

        [DataMember]
        public int id;

        [DataMember]
        public bool is_reply_comment;
                
        [DataMember]
        public string message;

        [DataMember]
        public CommentingUser user;

        [DataMember]
        int created;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("type: {0} id: {1} is_reply_comment: {2} message: {3} created: {4}",
                            type, id, is_reply_comment, message, created);
            return sb.ToString();
        }
    }
}