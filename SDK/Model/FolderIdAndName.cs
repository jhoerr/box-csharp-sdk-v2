using System.Runtime.Serialization;

namespace BoxApi.V2.SDK.Model
{
    [DataContract]
    public class FolderIdAndName
    {
        [DataMember]
        public int folder_id;

        [DataMember]
        public string name;
    }
}