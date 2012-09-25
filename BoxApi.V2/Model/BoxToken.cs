using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxApi.V2.Model
{
    class BoxToken
    {
        /// <summary>
        /// Type of item.  One of: File, Folder, Comment, Discussion, Event, BoxToken
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The items's ID
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// A unique ID for use with the /events endpoint
        /// </summary>
        public Entity Item { get; set; }
    }
}
