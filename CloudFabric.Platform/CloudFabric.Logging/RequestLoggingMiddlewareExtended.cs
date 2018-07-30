using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace CloudFabric.Logging
{
    public class RequestLoggingMiddlewareExtended
    {
        private readonly RequestDelegate _next;
        private TelemetryClient _telemetryClient { get; set; }
        private readonly ILogger<RequestLoggingMiddlewareExtended> _logger;

        public RequestLoggingMiddlewareExtended(RequestDelegate next, ILogger<RequestLoggingMiddlewareExtended> logger, TelemetryClient telemetryClient)
        {
            _next = next;
            _logger = logger;
            _telemetryClient = telemetryClient;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var requestBody = new StreamReader(context.Request.Body).ReadToEnd();

                var bodyStream = context.Response.Body;
                var responseBodyStream = new MemoryStream();
                context.Response.Body = responseBodyStream;

                if (context.Request.Path != null && context.Request.Path != "/"
                                                 && !context.Request.Path.ToString().Contains(".ico")
                                                 && !context.Request.Path.ToString().Contains(".css")
                                                 && !context.Request.Path.ToString().Contains(".html")
                                                 && !context.Request.Path.ToString().Contains(".js")
                                                 && !context.Request.Path.ToString().Contains("images")
                                                 && !context.Request.Path.ToString().Contains("fonts")
                                                 && !context.Request.Path.ToString().Contains("font-awesome")
                                                 && !context.Request.Path.ToString().Contains(".well-known"))
                {
                    var logTemplate = @" 
                            Client IP: {clientIP} 
                            Request path: {requestPath} 
                            Request content type: {requestContentType} 
                            Request content length: {requestContentLength} 
                            RequestBody: {requestbody}";

                    _telemetryClient.TrackTrace(
                                                requestBody,
                                                SeverityLevel.Information,
                                                new Dictionary<string, string> {
                                                { "RequestType", "HTTP Request Logging" }
                                                }
                                               );

                    _logger.LogInformation(logTemplate,
                        context.Connection.RemoteIpAddress.ToString(),
                        context.Request.Path,
                        context.Request.ContentType,
                        context.Request.ContentLength,
                        requestBody);
                }
                context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));

                await _next.Invoke(context);

                responseBodyStream.Seek(0, SeekOrigin.Begin);
                var responseBody = new StreamReader(responseBodyStream).ReadToEnd();

                if (!string.IsNullOrWhiteSpace(responseBody) && context.Request.Path != null
                                                             && context.Request.Path != "/"
                                                             && !context.Request.Path.ToString().Contains(".ico")
                                                             && !context.Request.Path.ToString().Contains(".css")
                                                             && !context.Request.Path.ToString().Contains(".html")
                                                             && !context.Request.Path.ToString().Contains(".js")
                                                             && !context.Request.Path.ToString().Contains("images")
                                                             && !context.Request.Path.ToString().Contains("fonts")
                                                             && !context.Request.Path.ToString().Contains("font-awesome")
                                                             && !context.Request.Path.ToString().Contains(".well-known"))
                {
                    var logResponseTemplate = @" 
                            Client IP: {clientIP} 
                            Response content type: {responseContentType} 
                            Response content length: {responseContentLength} 
                            ResponseBody: {responsebody}";

                    _telemetryClient.TrackTrace(
                                                responseBody,
                                                SeverityLevel.Information,
                                                new Dictionary<string, string> {
                                                { "RequestType", "HTTP Response Logging" }
                                                }
                                               );

                    _logger.LogInformation(logResponseTemplate,
                        context.Connection.RemoteIpAddress.ToString(),
                        context.Response.ContentType,
                        context.Response.ContentLength,
                        responseBody);
                }
                if (context.Response.StatusCode != 204 && !string.IsNullOrWhiteSpace(responseBody))
                {
                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    await responseBodyStream.CopyToAsync(bodyStream);
                    context.Response.Body = bodyStream;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message + Environment.NewLine + ex.StackTrace);
                _telemetryClient.TrackException(ex);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
    }
}
