using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bandwidth.Iris.Tests
{
    public sealed class HttpServer: IDisposable
    {
        private readonly RequestHandler[] _handlers;
        private readonly HttpListener _listener;

        public int RequestCount { get; private set; }

        public HttpServer(RequestHandler handler, string prefix = null)
            : this(new[] { handler }, prefix)
        {
        }

        public HttpServer(RequestHandler[] handlers, string prefix = null)
        {
            if (handlers == null) throw new ArgumentNullException("handlers");
            _handlers = handlers;
            _listener = new HttpListener();
            _listener.Prefixes.Add(prefix ?? "http://localhost:3001/");
            _listener.Start();
            RequestCount = 0;
            StartHandleRequest();
        }
        public void Dispose()
        {
            ((IDisposable)_listener).Dispose();
        }

        private Task StartHandleRequest()
        {
            return _listener.GetContextAsync().ContinueWith(HandlerRequest).ContinueWith(t => StartHandleRequest());
        }

        private async void HandlerRequest(Task<HttpListenerContext> obj)
        {
            if(obj.Status == TaskStatus.Faulted) return;
            var context = obj.Result;
            var handler = GetRequestHandler();
            try
            {
                if (!_listener.IsListening) return;
                var request = context.Request;
                var response = context.Response;
                if (handler.EstimatedMethod != null)
                {
                    Assert.Equal(handler.EstimatedMethod, request.HttpMethod);
                }
                if (handler.EstimatedPathAndQuery != null)
                {
                    Assert.Equal(handler.EstimatedPathAndQuery, request.Url.PathAndQuery);
                }
                if (handler.EstimatedContent != null)
                {
                    using (var reader = new StreamReader(request.InputStream, Encoding.UTF8))
                    {
                        var content = reader.ReadToEnd();
                        Assert.Equal(handler.EstimatedContent.Replace(Environment.NewLine, ""), content.Replace(Environment.NewLine, "")); 
                    }
                }
                if (handler.EstimatedHeaders != null)
                {
                    foreach (var estimatedHeader in handler.EstimatedHeaders)
                    {
                        Assert.Equal(estimatedHeader.Value, request.Headers[estimatedHeader.Key]);
                    }
                }
                response.StatusCode = handler.StatusCodeToSend;
                if (handler.HeadersToSend != null)
                {
                    foreach (var header in handler.HeadersToSend)
                    {
                        response.AddHeader(header.Key, header.Value);
                    }
                }
                if (handler.ContentToSend != null)
                {
                    foreach (var header in handler.ContentToSend.Headers)
                    {
                        foreach (var val in header.Value)
                        {
                            response.AddHeader(header.Key, val);
                        }
                    }
                    await handler.ContentToSend.CopyToAsync(response.OutputStream);
                }

                response.Close();
            }
            catch(Exception ex)
            {
                context.Response.Close();
                _errors.Add(ex);
            }
            RequestCount ++;
        }

        private RequestHandler GetRequestHandler()
        {
            if (RequestCount >= _handlers.Length)
            {
                return _handlers[_handlers.Length - 1];
            }
            return _handlers[RequestCount];
        }

        public Exception Error
        {
            get
            {
                return _errors.Count > 0 ? _errors[_errors.Count - 1] : null;
            }
        }

        private readonly List<Exception> _errors = new List<Exception>();
    }

    public class RequestHandler
    {
        public RequestHandler()
        {
            StatusCodeToSend = 200;
            EstimatedMethod = "GET";
        }
        public string EstimatedMethod { get; set; }
        public string EstimatedPathAndQuery { get; set; }
        public string EstimatedContent { get; set; }
        public Dictionary<string, string> EstimatedHeaders { get; set; }

        public Dictionary<string, string> HeadersToSend { get; set; }
        public HttpContent ContentToSend { get; set; }
        public int StatusCodeToSend { get; set; }
    }
}
