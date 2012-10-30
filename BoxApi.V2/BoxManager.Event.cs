using System.Globalization;
using BoxApi.V2.Model;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        public long GetCurrentStreamPosition()
        {
            var theEvent = GetEvents("now", StreamType.All, 1);
            return theEvent.NextStreamPosition;
        }

        public Event GetEvents(long streamPosition, StreamType streamType, int limit)
        {
            return GetEvents(streamPosition.ToString(CultureInfo.InvariantCulture), streamType, limit);
        }

        private Event GetEvents(string streamPosition, StreamType streamType, int limit)
        {
            var request = _requestHelper.GetEvents(streamPosition, streamType, limit);
            return _restClient.ExecuteAndDeserialize<Event>(request);
        }
    }
}