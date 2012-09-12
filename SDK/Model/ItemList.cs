using System.Runtime.Serialization;
using System.Text;

namespace BoxApi.V2.SDK.Model
{
    [DataContract]
    public class ItemList
    {
        [DataMember]
        public Item[] items;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

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