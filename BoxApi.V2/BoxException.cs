using System;
using System.Net;
using BoxApi.V2.Model;

namespace BoxApi.V2
{
    /// <summary>
    ///     Describes an error that occured when performing a request to the Box API
    /// </summary>
    public class BoxException : Exception
    {
        /// <summary>
        ///     Creates a BoxException instance
        /// </summary>
        /// <param name="error">The error returned by Box following the failed API call</param>
        public BoxException(Error error)
        {
            Error = error;
        }

        /// <summary>
        ///     The HTTP status code of the error (eg, 500)
        /// </summary>
        public HttpStatusCode HttpStatusCode
        {
            get { return (HttpStatusCode) Error.Status; }
        }

        /// <summary>
        ///     A short description of the error
        /// </summary>
        public string ShortDescription
        {
            get { return Error.Code; }
        }

        /// <summary>
        ///     A full description of the error
        /// </summary>
        public new string Message
        {
            get { return Error.Message; }
        }

        /// <summary>
        ///     The raw error information, as return by Box
        /// </summary>
        public Error Error { get; private set; }

        public override string ToString()
        {
            return string.Format("Box returned HTTP Code {0} ({1}): {2}", HttpStatusCode, ShortDescription, Message);
        }
    }

    /// <summary>
    ///     A special case exception for when the operation failed an If-Match precondition check.  This exception indicates that the target item has been modified since it was last retrieved.
    /// </summary>
    public class BoxItemModifiedException : BoxException
    {
        /// <summary>
        ///     Creates a BoxPreconditionException instance
        /// </summary>
        /// <param name="error">The error returned by Box following the failed API call</param>
        public BoxItemModifiedException(Error error) : base(error)
        {
        }
    }

    /// <summary>
    ///     A special case exception for when the operation failed an If-Not-Match precondition check.  This exception indicates that the requested item or collection has not been modified.
    /// </summary>
    public class BoxItemNotModifiedException : BoxException
    {
        /// <summary>
        ///     Creates a BoxItemNotModifiedException instance
        /// </summary>
        /// <param name="error">The error returned by Box following the failed API call</param>
        public BoxItemNotModifiedException(Error error)
            : base(error)
        {
        }
    }

    /// <summary>
    ///     A special case exception for when the requested file is not yet ready for download.  Wait at least the amount of time in 'RetryAfter' before trying again.
    /// </summary>
    public class BoxDownloadNotReadyException : Exception
    {
        /// <summary>
        ///     Creates a BoxDownloadNotReadyException instance
        /// </summary>
        public BoxDownloadNotReadyException(int retryAfter)
        {
            RetryAfter = retryAfter;
        }

        /// <summary>
        /// The amount of time, in seconds, after which the download will be ready.
        /// </summary>
        public int RetryAfter { get; private set; }
    }
}