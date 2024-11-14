using System;
using System.Net;
using System.Xml.Linq;

namespace Bandwidth.Iris
{
    public sealed class BandwidthIrisException : Exception
    {
        public string Code { get; private set; }
        public HttpStatusCode HttpStatusCode { get; private set; }
        public XDocument Body { get; private set; }

        public BandwidthIrisException(string code, string message, HttpStatusCode statusCode, XDocument body = null) : base(message)
        {
            Code = code;
            HttpStatusCode = statusCode;
            Body = body;
        }
    }

}
