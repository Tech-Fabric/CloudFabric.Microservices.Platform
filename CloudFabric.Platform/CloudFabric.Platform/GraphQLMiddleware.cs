﻿using System;
using GraphQL;
using System.IO;
using GraphQL.Http;
using GraphQL.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CloudFabric.Platform
{
    public class GraphQLMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDocumentWriter _writer;
        private readonly IDocumentExecuter _executor;

        public GraphQLMiddleware(RequestDelegate next, IDocumentWriter writer, IDocumentExecuter executor)
        {
            _next = next;
            _writer = writer;
            _executor = executor;
        }

        public async Task InvokeAsync(HttpContext httpContext, ISchema schema, IServiceProvider serviceProvider)
        {
            if (httpContext.Request.Path.StartsWithSegments("/api/graphql") && string.Equals(httpContext.Request.Method, "POST", StringComparison.OrdinalIgnoreCase))
            {
                string body;
                using (var streamReader = new StreamReader(httpContext.Request.Body))
                {
                    body = await streamReader.ReadToEndAsync();

                    var request = JsonConvert.DeserializeObject<GraphQLRequest>(body);

                    var result = await _executor.ExecuteAsync(doc =>
                    {
                        doc.Schema = schema;
                        doc.Query = request.Query;
                        doc.Inputs = request.Variables.ToInputs();
                    }).ConfigureAwait(false);

                    var json = _writer.Write(result);
                    await httpContext.Response.WriteAsync(json);
                }
            }
            else
            {
                await _next(httpContext);
            }
        }
    }

    public class GraphQLRequest
    {
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}