using EmpMgmt.Core.Models;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Text.Json;
using FluentValidation;

namespace EmpMgmt.WebAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string message;

            switch (exception)
            {
                case SqlException:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "An error occurred while processing your request. Please contact support for more details.";
                    break;
                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = exception.Message;
                    break;
                case ValidationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred. Please try again later.";
                    break;
            }

            var response = new ErrorResponse
            {
                IsSuccess = false,
                Message = message,
                StatusCode = (int)statusCode
            };

            var responsePayload = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(responsePayload);
        }
    }
}
