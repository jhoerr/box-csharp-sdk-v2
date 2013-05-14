using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoxApi.V2.Model.Enum;
using Newtonsoft.Json;

namespace BoxApi.V2.Model
{
    /// <summary>
    ///     A Box folder, which can contain files and other folders
    /// </summary>
    public class Folder : File
    {
        public const string Root = "0";

        /// <summary>
        ///     An array of file or folder objects contained in this folder
        /// </summary>
        [JsonProperty(PropertyName = "item_collection")]
        public ItemCollection ItemCollection { get; set; }

        /// <summary>
        /// The upload email address for this folder
        /// </summary>
        public FolderUploadEmail FolderUploadEmail { get; set; }

        /// <summary>
        ///     The files contained within this folder
        /// </summary>
        [JsonIgnore]
        public IEnumerable<File> Files
        {
            get { return FromEntriesGetAll<File>(ResourceType.File); }
        }

        /// <summary>
        /// Whether this folder will be synced by the Box sync clients or not.  This field is not returned by default; it must be explicitly requested via the 'fields' parameter.
        /// </summary>
        [JsonProperty(PropertyName = "sync_state")]
        public SyncState SyncState { get; set; }

        /// <summary>
        ///     The subfolders contained within this folder
        /// </summary>
        [JsonIgnore]
        public IEnumerable<Folder> Folders
        {
            get { return FromEntriesGetAll<Folder>(ResourceType.Folder); }
        }

        private IEnumerable<T> FromEntriesGetAll<T>(ResourceType value)
        {
            return ItemCollection.Entries.Where(i => i.Type.Equals(value)).Cast<T>();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("type: {0} id: {1} created_by: {2} created_at: {3} modified_by: {4} modified_at: {5} owned_by: {6} name: {7} description: {8} size: {9}",
                            Type, Id, CreatedBy, CreatedAt.HasValue ? CreatedAt.ToString() : "null", ModifedBy, ModifiedAt.HasValue ? ModifiedAt.ToString() : "null", OwnedBy, Name, Description, Size);

            if (ItemCollection != null)
            {
                foreach (var item in ItemCollection.Entries)
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