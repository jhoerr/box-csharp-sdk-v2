using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxApi.V2.Model
{
    /// <summary>
    /// The search results of a user's Box
    /// </summary>
    public class SearchResultCollection : Collection<Entity>
    {
        /// <summary>
        /// The search result at which the response was started
        /// </summary>
        public int Offset { get; set; }
    }
}
