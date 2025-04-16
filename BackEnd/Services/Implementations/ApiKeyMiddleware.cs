using BackEnd.Services.Interfaces;

namespace BackEnd.Services.Implementations
{
    public class ApiKeyMiddleware: IApiKeyMiddleware
    {

        RequestDelegate _requestDelegate;
        const string APIKEYNAME = "ApiKey";

        public ApiKeyMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extratedValue))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key is missing");
                return;
            }

            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(APIKEYNAME);

            if (!apiKey.Equals(extratedValue))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key is invalid");
                return;
            }

            await _requestDelegate(context);
        }
    }
}
