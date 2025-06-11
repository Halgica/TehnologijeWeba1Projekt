using Microsoft.AspNetCore.Authorization;

// We aint using this
namespace ReservationAPI.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _requiredRole;

        public AuthorizationMiddleware(RequestDelegate next, string requiredRole)
        {
            _next = next;
            _requiredRole = requiredRole;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Skip authorization if endpoint has [AllowAnonymous] attribute
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                await _next(context);
                return;
            }

            // Check if user is authenticated
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized - Please log in");
                return;
            }

            // Check if user has the required role
            if (!context.User.IsInRole(_requiredRole))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync($"Forbidden - Requires {_requiredRole} role");
                return;
            }

            // User is authorized, continue to next middleware
            await _next(context);
        }
    }
}
