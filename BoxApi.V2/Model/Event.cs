using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxApi.V2.Model
{
    /// <summary>
    /// A collection of 
    /// </summary>
    public class Event
    {
        /// <summary>
        /// The number of event records returned
        /// </summary>
        public int ChunkSize { get; set; }

        /// <summary>
        /// The next position in the event stream that you should request in order to get the next events
        /// </summary>
        public long NextStreamPosition { get; set; }

        /// <summary>
        /// A collection of events that occured
        /// </summary>
        public List<EventEntry> Entries { get; set; }
    }
}
