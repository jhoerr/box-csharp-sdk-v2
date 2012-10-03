using System.Collections.Generic;

namespace BoxApi.V2.Model
{
    public class Collection<T> where T: class, new()
    {
        /// <summary>
        /// Count of Entries
        /// </summary>
        public string TotalCount { get; set; }

        /// <summary>
        /// A collection of T items
        /// </summary>
        public List<T> Entries { get; set; } 
    }
}