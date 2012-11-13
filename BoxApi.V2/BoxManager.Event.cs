using System;
using System.Globalization;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        /// <summary>
        ///     Gets the latest event stream position for a user.  When used with GetUserEvents it ensures that events prior to 'now' are not returned.
        /// </summary>
        /// <returns>An event stream position</returns>
        public long GetCurrentStreamPosition()
        {
            return GetUserEvents("now", StreamType.All, 1).NextStreamPosition;
        }

        /// <summary>
        ///     Gets the latest event stream position for a user.  When used with GetUserEvents it ensures that events prior to 'now' are not returned.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the event stream position</param>
        /// <param name="onFailure">Action to perform following a failed Event operation</param>
        public void GetCurrentStreamPosition(Action<long> onSuccess, Action<Error> onFailure)
        {
            var request = _requestHelper.GetUserEvents("now", StreamType.All, 1);
            Action<UserEventCollection> onSuccessWrapper = e => onSuccess(e.NextStreamPosition);
            _restClient.ExecuteAsync(request, onSuccessWrapper, onFailure);
        }

        /// <summary>
        ///     Retrieves of events that have occured in a user's account.  Events will occasionally arrive out of order.   You may need to buffer events and apply them in a logical order.
        /// </summary>
        /// <param name="streamPosition">The stream position from which to start receiving events.  If left to the default value the earliest events on record for the user will be returned, even if they have happened in the distant past.  Consider using GetCurrentStreamPosition to get a reasonable initial stream position.</param>
        /// <param name="streamType">Filters the type of events to return. Default is All events (no filter)</param>
        /// <param name="limit">The maximum number of events to return with this request. Default is 100.</param>
        /// <returns>A collection of UserEvents</returns>
        /// <remarks>You are then expected to call GetUserEvents endpoint with ever increasing streamPosition values, as given back to you with each response, until you get no more events.</remarks>
        public UserEventCollection GetUserEvents(long streamPosition = 0, StreamType streamType = StreamType.All, int limit = 100)
        {
            return GetUserEvents(streamPosition.ToString(CultureInfo.InvariantCulture), streamType, limit);
        }

        private UserEventCollection GetUserEvents(string streamPosition, StreamType streamType, int limit)
        {
            var request = _requestHelper.GetUserEvents(streamPosition, streamType, limit);
            return _restClient.ExecuteAndDeserialize<UserEventCollection>(request);
        }

        /// <summary>
        ///     Retrieves of events that have occured in a user's account.  Events will occasionally arrive out of order.   You may need to buffer events and apply them in a logical order.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved events.</param>
        /// <param name="onFailure">Action to perform following a failed Event operation</param>
        /// <param name="streamPosition">The stream position from which to start receiving events.  If left to the default value the earliest events on record for the user will be returned, even if they have happened in the distant past.  Consider using GetCurrentStreamPosition to get a reasonable initial stream position.</param>
        /// <param name="streamType">Filters the type of events to return. Default is All events (no filter)</param>
        /// <param name="limit">The maximum number of events to return with this request. Default is 100.</param>
        /// <remarks>You are then expected to call GetUserEvents endpoint with ever increasing streamPosition values, as given back to you with each response, until you get no more events.</remarks>
        public void GetUserEvents(Action<UserEventCollection> onSuccess, Action<Error> onFailure, long streamPosition = 0, StreamType streamType = StreamType.All, int limit = 100)
        {
            var request = _requestHelper.GetUserEvents(streamPosition.ToString(CultureInfo.InvariantCulture), streamType, limit);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Retrieves the events that have occurred in an enterprise.  Events will occasionally arrive out of order.  You may need to buffer events and apply them in a logical order.
        /// </summary>
        /// <param name="offset">The item at which to start.  Deault is 0 (the earliest known event.)</param>
        /// <param name="limit">The maximum number of events to return with this request. Default is 100.</param>
        /// <param name="createdAfter">A lower bound on the timestamp of the events returned</param>
        /// <param name="createdBefore">An upper bound on the timestamp of the events returned</param>
        /// <param name="eventTypes">A list of event types to filter by.  Only events of these types will be returned.</param>
        /// <returns>A collection of EnterpriseEvents.</returns>
        public EnterpriseEventCollection GetEnterpriseEvents(int offset = 0, int limit = 100, DateTime? createdAfter = null, DateTime? createdBefore = null, EnterpriseEventType[] eventTypes = null)
        {
            var request = _requestHelper.GetEnterpriseEvents(offset, limit, createdAfter, createdBefore, eventTypes);
            return _restClient.ExecuteAndDeserialize<EnterpriseEventCollection>(request);
        }

        /// <summary>
        ///     Retrieves the events that have occurred in an enterprise.  Events will occasionally arrive out of order.  You may need to buffer events and apply them in a logical order.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved events</param>
        /// <param name="onFailure">Action to perform following a failed Event operation</param>
        /// <param name="offset">The item at which to start.  Deault is 0 (the earliest known event.)</param>
        /// <param name="limit">The maximum number of events to return with this request. Default is 100.</param>
        /// <param name="createdAfter">A lower bound on the timestamp of the events returned</param>
        /// <param name="createdBefore">An upper bound on the timestamp of the events returned</param>
        /// <param name="eventTypes">A list of event types to filter by.  Only events of these types will be returned.</param>
        public void GetEnterpriseEvents(Action<EnterpriseEventCollection> onSuccess, Action<Error> onFailure, int offset = 0, int limit = 100, DateTime? createdAfter = null,
                                        DateTime? createdBefore = null, EnterpriseEventType[] eventTypes = null)
        {
            var request = _requestHelper.GetEnterpriseEvents(offset, limit, createdAfter, createdBefore, eventTypes);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}