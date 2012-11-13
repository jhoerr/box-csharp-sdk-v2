using System.ComponentModel;

namespace BoxApi.V2.Model.Enum
{
    public enum StreamType
    {
        /// <summary>
        ///   Returns all event types
        /// </summary>
        [Description("all")] All,

        /// <summary>
        ///   Returns tree changes
        /// </summary>
        [Description("changes")] Changes,

        /// <summary>
        ///   Returns tree changes only for sync folders
        /// </summary>
        [Description("sync")] Sync,

        /// <summary>
        ///   Returns administrative events for Enterprise accounts.
        /// </summary>
        [Description("admin_logs")] AdminLogs,
    }
}