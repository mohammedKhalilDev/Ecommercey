using Ecom.API.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace Ecom.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env, IMemoryCache memoryCache)
        {
            _next = next;
            _env = env;
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                ApplySecurity(context);

                if (!IsRequestAllowed(context))
                {

                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";
                    var responce = new ApiException((int)HttpStatusCode.TooManyRequests, "Too Many Requests please try again later");

                    await context.Response.WriteAsJsonAsync(responce);
                }

                await _next(context);

            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var responce = _env.IsDevelopment() ?
                    new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new ApiException((int)HttpStatusCode.InternalServerError, ex.Message);

                var json = JsonSerializer.Serialize(responce);
                await context.Response.WriteAsync(json);
            }
        }

        private bool IsRequestAllowed(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress.ToString();
            var cashKey = $"Rate:{ip}";
            var dateNow = DateTime.Now;

            var (timesStamp, count) = _memoryCache.GetOrCreate(cashKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
                return (timesStamp: dateNow, count: 0);
            });
            var diff = dateNow - timesStamp;
            if (diff < _rateLimitWindow)
            {
                if (count >= 8)
                {
                    return false;
                }

                _memoryCache.Set(cashKey, (timesStamp, count += 1), _rateLimitWindow);
            }
            else
            {
                _memoryCache.Set(cashKey, (timesStamp, count), _rateLimitWindow);
            }
            return true;

        }

        private void ApplySecurity(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-XSS-Protection"] = "1;mode=block";
            context.Response.Headers["X-Frame-Options"] = "DENY";
        }
    }
}
