using System.Collections.Generic;

namespace BoxApi.V2.SDK.Model
{
    public class Collection
    {
        /// <summary>
        /// Count of Entries
        /// </summary>
        public string TotalCount { get; set; }
    }

    /// <summary>
    /// Provides basic information about a folder's contents
    /// </summary>
    public class ItemCollection : Collection
    {
        /// <summary>
        /// An array of file and folder objects contained in this folder
        /// </summary>
        public List<File> Entries { get; set; }
    }

    public class ErrorCollection : Collection
    {
        /// <summary>
        /// An array of file and folder objects contained in this folder
        /// </summary>
        public List<Error> Entries { get; set; }

    }
}