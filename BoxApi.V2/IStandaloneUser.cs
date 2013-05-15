using System;
using System.Collections.Generic;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Model.Fields;

namespace BoxApi.V2
{
    /// <summary>
    /// Methods for working with information and events for the current Box user
    /// </summary>
    public interface IStandaloneUser
    {
        /// <summary>
        ///     Retrieves information about the user who is currently logged in i.e. the user for whom this auth token was generated. 
        /// </summary>
        /// <param name="fields">The properties that should be set on the returned User.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>Returns a single complete user object. An error is returned if a valid auth token is not included in the API request.</returns>
        User Me(IEnumerable<UserField> fields = null);

        /// <summary>
        ///     Retrieves information about the user who is currently logged in i.e. the user for whom this auth token was generated. 
        /// </summary>
        /// <param name="onSuccess">Action to perform with the current user</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="fields">The properties that should be set on the returned User.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void Me(Action<User> onSuccess, Action<Error> onFailure, IEnumerable<UserField> fields = null);

        /// <summary>
        ///     Gets the latest event stream position for a user.  When used with GetUserEvents it ensures that events prior to 'now' are not returned.
        /// </summary>
        /// <returns>An event stream position</returns>
        long GetCurrentStreamPosition();

        /// <summary>
        ///     Gets the latest event stream position for a user.  When used with GetUserEvents it ensures that events prior to 'now' are not returned.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the event stream position</param>
        /// <param name="onFailure">Action to perform following a failed Event operation</param>
        void GetCurrentStreamPosition(Action<long> onSuccess, Action<Error> onFailure);

        /// <summary>
        ///     Retrieves of events that have occured in a user's account.  Events will occasionally arrive out of order.   You may need to buffer events and apply them in a logical order.
        /// </summary>
        /// <param name="streamPosition">The stream position from which to start receiving events.  If left to the default value the earliest events on record for the user will be returned, even if they have happened in the distant past.  Consider using GetCurrentStreamPosition to get a reasonable initial stream position.</param>
        /// <param name="streamType">Filters the type of events to return. Default is All events (no filter)</param>
        /// <param name="limit">The maximum number of events to return with this request. Default is 100.</param>
        /// <returns>A collection of UserEvents</returns>
        /// <remarks>You are then expected to call GetUserEvents endpoint with ever increasing streamPosition values, as given back to you with each response, until you get no more events.</remarks>
        UserEventCollection GetUserEvents(long streamPosition = 0, StreamType streamType = StreamType.All, int limit = 100);

        /// <summary>
        ///     Retrieves of events that have occured in a user's account.  Events will occasionally arrive out of order.   You may need to buffer events and apply them in a logical order.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved events.</param>
        /// <param name="onFailure">Action to perform following a failed Event operation</param>
        /// <param name="streamPosition">The stream position from which to start receiving events.  If left to the default value the earliest events on record for the user will be returned, even if they have happened in the distant past.  Consider using GetCurrentStreamPosition to get a reasonable initial stream position.</param>
        /// <param name="streamType">Filters the type of events to return. Default is All events (no filter)</param>
        /// <param name="limit">The maximum number of events to return with this request. Default is 100.</param>
        /// <remarks>You are then expected to call GetUserEvents endpoint with ever increasing streamPosition values, as given back to you with each response, until you get no more events.</remarks>
        void GetUserEvents(Action<UserEventCollection> onSuccess, Action<Error> onFailure, long streamPosition = 0, StreamType streamType = StreamType.All, int limit = 100);

        /// <summary>
        ///     Find items that are accessible in a given user’s Box account.
        /// </summary>
        /// <param name="query">The string to search for; can be matched against item names, descriptions, text content of a file, and other fields of the different item types.</param>
        /// <param name="limit">Number of search results to return. If both an offset and limit are specified, the offset must be a multiple of the limit.</param>
        /// <param name="offset">The search result at which to start the response. If both an offset and limit are specified, the offset must be a multiple of the limit.</param>
        /// <returns>A collection of items resulting from the search</returns>
        SearchResultCollection Search(string query, uint? limit = null, uint? offset = null);

        /// <summary>
        ///     Find items that are accessible in a given user’s Box account.
        /// </summary>
        /// <param name="query">The string to search for; can be matched against item names, descriptions, text content of a file, and other fields of the different item types.</param>
        /// <param name="onSuccess">Action to perform following a successful search</param>
        /// <param name="onFailure">Action to perform following a failed search</param>
        /// <param name="limit">Number of search results to return. If both an offset and limit are specified, the offset must be a multiple of the limit.</param>
        /// <param name="offset">The search result at which to start the response. If both an offset and limit are specified, the offset must be a multiple of the limit.</param>
        void Search(Action<SearchResultCollection> onSuccess, Action<Error> onFailure, string query, uint? limit = null, uint? offset = null);
    }
}