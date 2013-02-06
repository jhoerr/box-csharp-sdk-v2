using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BoxApi.V2.Model.Enum
{
    public enum SyncState
    {
        /// <summary>
        /// The sync state of the folder has not been requested
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// The folder is not synced with the server
        /// </summary>
        [EnumMember(Value = "not_synced")]NotSynced,
        /// <summary>
        /// The folder is partially synced with the server
        /// </summary>
        [EnumMember(Value = "partially_synced")]PartiallySynced,
        /// <summary>
        /// The folder is fully synced with the server
        /// </summary>
        [EnumMember(Value = "synced")]Synced,
    }
}
