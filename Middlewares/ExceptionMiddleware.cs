using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using VersionamentoApi.Controllers;

namespace VersionamentoApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            var code = (int)HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is not Exception) return Task.CompletedTask;

            var result = JsonSerializer.Serialize(new ResponseError(
                code,
                " =( ",
                "Erro interno do Servidor"
            ));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            return context.Response.WriteAsync(result);
        }
    }
}
