using System;
using System.Globalization;
using BoxApi.V2.Model;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        public long GetCurrentStreamPosition()
        {
            return GetUserEvents("now", StreamType.All, 1).NextStreamPosition;
        }

        public void GetCurrentStreamPosition(Action<long> onSuccess, Action<Error> onFailure)
        {
            var request = _requestHelper.GetEvents("now", StreamType.All, 1);
            Action<Event> onSuccessWrapper = e => onSuccess(e.NextStreamPosition);
            _restClient.ExecuteAsync(request, onSuccessWrapper, onFailure);
        }

        public Event GetUserEvents(long streamPosition = 0, StreamType streamType = StreamType.All, int limit = 100)
        {
            return GetUserEvents(streamPosition.ToString(CultureInfo.InvariantCulture), streamType, limit);
        }

        private Event GetUserEvents(string streamPosition, StreamType streamType, int limit)
        {
            var request = _requestHelper.GetEvents(streamPosition, streamType, limit);
            return _restClient.ExecuteAndDeserialize<Event>(request);
        }

        public void GetUserEvents(long streamPosition, StreamType streamType, int limit, Action<Event> onSuccess, Action<Error> onFailure)
        {
            var request = _requestHelper.GetEvents(streamPosition.ToString(CultureInfo.InvariantCulture), streamType, limit);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}