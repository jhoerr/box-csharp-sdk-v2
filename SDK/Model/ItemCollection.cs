using System.Collections.Generic;

namespace BoxApi.V2.SDK.Model
{
    /// <summary>
    /// Provides basic information about a folder's contents
    /// </summary>
    public class ItemCollection
    {
        /// <summary>
        /// Count of Entries
        /// </summary>
        public string TotalCount { get; set; }

        /// <summary>
        /// An array of file and folder objects contained in this folder
        /// </summary>
        public List<File> Entries { get; set; }
    }
}