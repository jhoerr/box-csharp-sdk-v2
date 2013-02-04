using System;
using BoxApi.V2.Model;
using RestSharp;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        /// <summary>
        ///     Find items that are accessible in a given user’s Box account.
        /// </summary>
        /// <param name="query">The string to search for; can be matched against item names, descriptions, text content of a file, and other fields of the different item types.</param>
        /// <param name="limit">Number of search results to return. If both an offset and limit are specified, the offset must be a multiple of the limit.</param>
        /// <param name="offset">The search result at which to start the response. If both an offset and limit are specified, the offset must be a multiple of the limit.</param>
        /// <returns>A collection of items resulting from the search</returns>
        public SearchResultCollection Search(string query, uint? limit = null, uint? offset = null)
        {
            GuardFromNull(query, "query");
            EnsureOffsetIsMultipleOfLimit(limit, offset);
            IRestRequest request = _requestHelper.Search(query, limit, offset);
            return _restClient.ExecuteAndDeserialize<SearchResultCollection>(request);
        }

        /// <summary>
        ///     Find items that are accessible in a given user’s Box account.
        /// </summary>
        /// <param name="query">The string to search for; can be matched against item names, descriptions, text content of a file, and other fields of the different item types.</param>
        /// <param name="onSuccess">Action to perform following a successful search</param>
        /// <param name="onFailure">Action to perform following a failed search</param>
        /// <param name="limit">Number of search results to return. If both an offset and limit are specified, the offset must be a multiple of the limit.</param>
        /// <param name="offset">The search result at which to start the response. If both an offset and limit are specified, the offset must be a multiple of the limit.</param>
        public void Search(Action<SearchResultCollection> onSuccess, Action<Error> onFailure, string query, uint? limit = null, uint? offset = null)
        {
            GuardFromNull(query, "query");
            EnsureOffsetIsMultipleOfLimit(limit, offset);
            IRestRequest request = _requestHelper.Search(query, limit, offset);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        /// Ensures that offset is an even multiple of limit if both are nonzero.
        /// </summary>
        /// <param name="limit">Number of search results to return.</param>
        /// <param name="offset">The search result at which to start the response.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void EnsureOffsetIsMultipleOfLimit(uint? limit, uint? offset)
        {
            if (offset != null && limit != null && offset != 0 && limit != 0 && offset % limit != 0)
            {
                throw new ArgumentException("The offset must be a multiple of limit", "offset");
            }
        }
    }
}