namespace BackEnd.Services.Interfaces
{
    public interface IApiKeyMiddleware
    {
        Task InvokeAsync(HttpContext context);
    }
}
