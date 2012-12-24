namespace BoxApi.V2.Model
{
    /// <summary>
    ///     2xx: The request was successfully received, understood, and accepted
    ///     3xx: Further action needs to be taken by the user agent in order to fulfill the request
    ///     4xx: An error in the request. Usually a bad parameter.
    ///     5xx: The request is fine, but something is wrong on Box’s end
    /// </summary>
    public class Error : EntityBase
    {
        /// <summary>
        ///     the HTTP status of the error response
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     the HTTP code of the error response
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     a URL linking to more information about why this error occurred
        /// </summary>
        public string HelpUrl { get; set; }

        /// <summary>
        ///     a human readable message about the error that can be passed back to your user
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     a unique ID for this request helpful for troubleshooting
        /// </summary>
        public string RequestId { get; set; }
    }

    public class AuthError
    {
        public string Error { get; set; }
        public string ErrorDescription { get; set; }
    }
}