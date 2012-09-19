using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxApi.V2.SDK.Model
{
    class BoxTicket
    {
        /// <summary>
        /// Status of the ticket request
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The ticket, if a valid one was returned 
        /// </summary>
        public string Ticket { get; set; }
    }
}
