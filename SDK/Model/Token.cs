using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxApi.V2.SDK.Model
{
    class Token
    {
        /// <summary>
        /// Type of item.  One of: File, Folder, Comment, Discussion, Event, Token
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The items's ID
        /// </summary>
        public string BoxToken { get; set; }

        /// <summary>
        /// A unique ID for use with the /events endpoint
        /// </summary>
        public Entity RootFolder { get; set; }
    }
}
