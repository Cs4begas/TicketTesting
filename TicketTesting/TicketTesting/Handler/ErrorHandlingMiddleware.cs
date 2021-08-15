using Hedwig.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TicketTesting.Handler
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Dictionary<ResponseException, HttpStatusCode> response = GetErrorMessage(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.First().Value;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response.First().Key));
        }

        public static Dictionary<ResponseException, HttpStatusCode> GetErrorMessage(Exception exception)
        {
            Dictionary<ResponseException, HttpStatusCode> response = new Dictionary<ResponseException, HttpStatusCode>();
            ResponseException responseException = new ResponseException();
            if (exception is NotFoundException)
            {
                NotFoundException notFoundException = (NotFoundException)exception;
                responseException.errorCode = notFoundException.code;
                responseException.errorMessage = notFoundException.message;
                response.Add(responseException, HttpStatusCode.NotFound);
            }
            else if (exception is ValidationException)
            {
                ValidationException validationException = (ValidationException)exception;
                responseException.errorCode = validationException.code;
                responseException.errorMessage = validationException.message;
                response.Add(responseException, HttpStatusCode.BadRequest);
            }
            else if (exception is UndefindedTypeException)
            {
                UndefindedTypeException undefindedTypeException = (UndefindedTypeException)exception;
                responseException.errorCode = undefindedTypeException.code;
                responseException.errorMessage = undefindedTypeException.message;
                response.Add(responseException, HttpStatusCode.BadRequest);
            }
            else
            {
                responseException.errorCode = "999999";
                responseException.errorMessage = exception.Message;
                response.Add(responseException, HttpStatusCode.InternalServerError);
            }
            return response;
        }

    }
}
