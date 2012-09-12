using System.Runtime.Serialization;
using System.Text;

namespace BoxApi.V2.SDK.Model
{
    [DataContract]
    public class Folder
    {
        [DataMember]
        public string type;

        [DataMember]
        public int id;

        [DataMember]
        UserIdAndName created_by;

        [DataMember]
        int? created_at;

        [DataMember]
        UserIdAndName modified_by;

        [DataMember]
        int? modified_at;
        
        [DataMember]
        UserIdAndName owned_by;

        [DataMember]
        public string name;

        [DataMember]
        public string description;

        [DataMember]
        public int? size;

        [DataMember]
        public int? trashed;

        [DataMember]
        public Item parent_folder;

        [DataMember]
        public Item[] items;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("type: {0} id: {1} created_by: {2} created_at: {3} modified_by: {4} modified_at: {5} owned_by: {6} name: {7} description: {8} size: {9} trashed: {10} parent_folder: ",
                            type, id, created_by, created_at.HasValue ? created_at.ToString() : "null", modified_by, modified_at.HasValue ? modified_at.ToString() : "null", owned_by, name, description, size, trashed.HasValue ? trashed.ToString() : "null");

            foreach (Item item in items)
            {
                if (item != null)
                {
                    sb.Append("\n\t");
                    sb.Append(item.ToString());
                }
            }
            return sb.ToString();
        }
    }
}