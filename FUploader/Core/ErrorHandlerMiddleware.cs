using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FUploader.Core
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private Notifocator _notifocator;


        public ErrorHandlerMiddleware(RequestDelegate next, Notifocator notifocator)
        {
            _next = next;
            _notifocator = notifocator;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                try 
                {
                    _notifocator.SendTelegramNotification(false, ex?.Message ?? "unknown error");
                    _notifocator.SendTelegramNotification(false, HttpUtility.UrlEncode(ex?.StackTrace) ?? "unknown trace");
                } 
                finally 
                {
                    httpContext.Response.StatusCode = 500;
                    await httpContext.Response.WriteAsJsonAsync(new { message = ex?.Message, trace = ex?.StackTrace });
                }
            }
        }
    }

}
