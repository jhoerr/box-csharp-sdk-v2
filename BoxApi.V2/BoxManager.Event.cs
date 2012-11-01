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
            var request = _requestHelper.GetUserEvents("now", StreamType.All, 1);
            Action<UserEventCollection> onSuccessWrapper = e => onSuccess(e.NextStreamPosition);
            _restClient.ExecuteAsync(request, onSuccessWrapper, onFailure);
        }

        public UserEventCollection GetUserEvents(long streamPosition = 0, StreamType streamType = StreamType.All, int limit = 100)
        {
            return GetUserEvents(streamPosition.ToString(CultureInfo.InvariantCulture), streamType, limit);
        }

        private UserEventCollection GetUserEvents(string streamPosition, StreamType streamType, int limit)
        {
            var request = _requestHelper.GetUserEvents(streamPosition, streamType, limit);
            return _restClient.ExecuteAndDeserialize<UserEventCollection>(request);
        }

        public void GetUserEvents(long streamPosition, StreamType streamType, int limit, Action<UserEventCollection> onSuccess, Action<Error> onFailure)
        {
            var request = _requestHelper.GetUserEvents(streamPosition.ToString(CultureInfo.InvariantCulture), streamType, limit);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public EnterpriseEventCollection GetEnterpriseEvents(int offset = 0, int limit = 100, DateTime? createdAfter = null, DateTime? createdBefore = null, EnterpriseEventType[] eventTypes = null)
        {
            var request = _requestHelper.GetEnterpriseEvents(offset, limit, createdAfter, createdBefore, eventTypes);
            return _restClient.ExecuteAndDeserialize<EnterpriseEventCollection>(request);
        }

        public void GetEnterpriseEvents(Action<EnterpriseEventCollection> onSuccess, Action<Error> onFailure, int offset = 0, int limit = 100, DateTime? createdAfter = null, DateTime? createdBefore = null, EnterpriseEventType[] eventTypes = null)
        {
            var request = _requestHelper.GetEnterpriseEvents(offset, limit, createdAfter, createdBefore, eventTypes);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}