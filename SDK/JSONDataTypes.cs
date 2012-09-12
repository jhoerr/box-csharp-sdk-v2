using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace BoxApi.V2
{
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
}


