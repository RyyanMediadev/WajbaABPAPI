using Microsoft.AspNetCore.Http;

namespace Wajba.Middleware
{
    public class BlockBuiltInEndpointsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _blockedPaths = new[]
        {
            "/api/users",
            "/api/users/", // To handle sub-paths like /api/users/{id}
            "/api/roles",
            "/api/roles/",
            "/api/permissions",
            "/api/permissions/",
            "/api/identity/users",
            "/api/identity/users/",
            "/api/identity/roles",
            "/api/identity/roles/",
            "/api/identity/permissions",
            "/api/identity/permissions/",
          
        };

        public BlockBuiltInEndpointsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestPath = context.Request.Path.Value;

            if (requestPath != null && _blockedPaths.Any(path => requestPath.StartsWith(path, StringComparison.OrdinalIgnoreCase)))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access to this endpoint is disabled.");
                return;
            }

            await _next(context);
        }
    }
}

