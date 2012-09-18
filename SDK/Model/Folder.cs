using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BoxApi.V2.SDK.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Folder : File
    {
        public const string Root = "0";

        /// <summary>
        ///   An array of file or folder objects contained in this folder
        /// </summary>
        [JsonProperty(PropertyName = "item_collection")]
        public ItemCollection ItemCollection { get; set; }

        public IEnumerable<File> Files { get { return ItemCollection.Entries.Where(i => i.Type.Equals("file")).Cast<File>(); } }
        public IEnumerable<Folder> Folders { get { return ItemCollection.Entries.Where(i => i.Type.Equals("file")); } }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("type: {0} id: {1} created_by: {2} created_at: {3} modified_by: {4} modified_at: {5} owned_by: {6} name: {7} description: {8} size: {9}",
                            Type, Id, CreatedBy, CreatedAt.HasValue ? CreatedAt.ToString() : "null", ModifedBy, ModifiedAt.HasValue ? ModifiedAt.ToString() : "null", OwnedBy, Name, Description, Size);

            if (ItemCollection != null)
            {
                foreach (Entity item in ItemCollection.Entries)
                {
                    if (item != null)
                    {
                        sb.Append("\n\t");
                        sb.Append(item);
                    }
                }
            }
            return sb.ToString();
        }
    }
}