using System;
using System.Net;
using BoxApi.V2.Model;

namespace BoxApi.V2
{
    public class BoxException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; private set; }
        public string ShortDescription { get { return Error.Code; } }
        public string Message { get { return Error.Message; } }
        public Error Error { get; private set; }

        public BoxException(Error error)
        {
            HttpStatusCode = (HttpStatusCode)int.Parse(error.Status);
            Error = error;
        }

        public override string ToString()
        {
            return string.Format("Box returned HTTP Code {0} ({1}): {2}", HttpStatusCode, ShortDescription, Message);
        }
    }
}