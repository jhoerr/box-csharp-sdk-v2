using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace BoxApi.V2
{
    [DataContract]
    public class JSONFolderIdAndName
    {
        [DataMember]
        public int folder_id;

        [DataMember]
        public string name;
    }

    [DataContract]
    public class JSONItem
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

    [DataContract]
    public class JSONItemList
    {
        [DataMember]
        public JSONItem[] items;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (JSONItem item in items)
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

    [DataContract]
    public class JSONUserIdAndName
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

    [DataContract]
    public class JSONUser
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

    [DataContract]
    public class JSONFile
    {
        [DataMember]
        public int file_id;

        [DataMember]
        public string name;

        [DataMember]
        public int createtime;

        [DataMember]
        public int updatetime;

        [DataMember]
        public int size;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("disc_id: {0} name: {1} create_time: {2} update_time: {3} size: {4}", 
                file_id, name, createtime, updatetime, size);
            return sb.ToString();
        }
    }

    [DataContract]
    public class JSONFolderInfo
    {
        [DataMember]
        public string type;

        [DataMember]
        public int id;

        [DataMember]
        JSONUserIdAndName created_by;

        [DataMember]
        int? created_at;

        [DataMember]
        JSONUserIdAndName modified_by;

        [DataMember]
        int? modified_at;
        
        [DataMember]
        JSONUserIdAndName owned_by;

        [DataMember]
        public string name;

        [DataMember]
        public string description;

        [DataMember]
        public int? size;

        [DataMember]
        public int? trashed;

        [DataMember]
        public JSONItem parent_folder;

        [DataMember]
        public JSONItem[] items;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("type: {0} id: {1} created_by: {2} created_at: {3} modified_by: {4} modified_at: {5} owned_by: {6} name: {7} description: {8} size: {9} trashed: {10} parent_folder: ",
                type, id, created_by, created_at.HasValue ? created_at.ToString() : "null", modified_by, modified_at.HasValue ? modified_at.ToString() : "null", owned_by, name, description, size, trashed.HasValue ? trashed.ToString() : "null");

            foreach (JSONItem item in items)
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

    [DataContract]
    public class JSONFileInfo
    {
        [DataMember]
        public string type;

        [DataMember]
        public int id;

        [DataMember]
        int? created_at;

        [DataMember]
        JSONUserIdAndName modified_by;

        [DataMember]
        int? modified_at;

        [DataMember]
        JSONUserIdAndName owned_by;

        [DataMember]
        public string name;

        [DataMember]
        public string description;

        [DataMember]
        public int? size;

        [DataMember]
        public int? trashed;

        [DataMember]
        public JSONItem parent_folder;

        [DataMember]
        public string sha1;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("type: {0} id: {1} sha1: {2} created_at: {3} modified_by: {4} modified_at: {5} owned_by: {6} name: {7} description: {8} size: {9} trashed: {10} parent_folder: ",
                type, id, sha1, created_at.HasValue ? created_at.ToString() : "null", modified_by, modified_at.HasValue ? modified_at.ToString() : "null", owned_by, name, description, size, trashed.HasValue ? trashed.ToString() : "null");

            return sb.ToString();
        }
    }

    [DataContract]
    public class JSONComment
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
        public JSONUser user;

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


