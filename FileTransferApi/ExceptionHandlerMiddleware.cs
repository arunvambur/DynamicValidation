using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FileTransferApi
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate)
        {
            this._requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                string result = string.Empty;
                switch (ex)
                {
                    case ValidationException validationException:

                        var errorResponse =
                            new
                            {
                                TraceId = context.TraceIdentifier,
                                Message = validationException.Message,
                                Errors = validationException.Errors.Select(t => new { PropertyName = t.PropertyName, ErrorMessage = t.ErrorMessage }) 
                            };

                        result = JsonConvert.SerializeObject(errorResponse);

                        break;

                    case FileTransferException fileTransferException:
                        var errorResponse1 =
                            new
                            {
                                TraceId = context.TraceIdentifier,
                                Message = fileTransferException.Message
                            };
                        result = JsonConvert.SerializeObject(errorResponse1);
                        break;
                }
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.WriteAsync(result);
            }
        }
    }
}
