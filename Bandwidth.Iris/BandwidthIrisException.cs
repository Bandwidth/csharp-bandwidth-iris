using System;
using System.Net;

namespace Bandwidth.Iris
{
    public sealed class BandwidthIrisException: Exception
    {
        public string Code { get; private set; }
        public HttpStatusCode HttpStatusCode { get; private set; }

        public BandwidthIrisException(string code, string message, HttpStatusCode statusCode): base(message)
        {
            Code = code;
            HttpStatusCode = statusCode;
        }
    }

}
