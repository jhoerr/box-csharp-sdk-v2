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
        ///     The HTTP status code of the error (eg, 500)
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; private set; }

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

        /// <summary>
        ///     Creates a BoxException instance
        /// </summary>
        /// <param name="error">The error returned by Box following the failed API call</param>
        public BoxException(Error error)
        {
            HttpStatusCode = (HttpStatusCode)error.Status;
        }

        public override string ToString()
        {
            return string.Format("Box returned HTTP Code {0} ({1}): {2}", HttpStatusCode, ShortDescription, Message);
        }
    }

    /// <summary>
    /// A special case exception for when the operation failed an If-Match precondition check.
    /// </summary>
    public class BoxPreconditionException : BoxException
    {
        /// <summary>
        ///     Creates a BoxPreconditionFailedException instance
        /// </summary>
        /// <param name="error">The error returned by Box following the failed API call</param>
        public BoxPreconditionException(Error error) : base(error)
        {
        }
    }
}