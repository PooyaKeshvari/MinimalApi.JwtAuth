using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using MinimalApi.JwtAuth.Application.Contracts;
using MinimalApi.JwtAuth.Application.Services;

namespace MinimalApi.JwtAuth.Presentation.Endpoints;

public static class IdentityEndpoints
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/token", (
            LoginRequest request,
            DemoUserStore userStore,
            ITokenService tokenService,
            IOptions<JwtOptions> jwtOptions) =>
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return Results.BadRequest(new
                {
                    title = "Invalid credentials payload",
                    status = 400,
                    detail = "Username and password are required."
                });
            }

            var user = userStore.Validate(request.Username, request.Password);
            if (user is null)
            {
                return Results.Unauthorized();
            }

            var token = tokenService.GenerateToken(user);
            return Results.Ok(new LoginResponse
            {
                AccessToken = token,
                TokenType = "Bearer",
                ExpiresInSeconds = jwtOptions.Value.ExpirationMinutes * 60,
                Username = user.Username,
                Role = user.Role
            });
        });

        app.MapGet("/api/profile/me", [Authorize] (ClaimsPrincipal principal) =>
        {
            var username = principal.FindFirstValue(ClaimTypes.Name) ?? "unknown";
            var role = principal.FindFirstValue(ClaimTypes.Role) ?? "unknown";

            return Results.Ok(new
            {
                Username = username,
                Role = role,
                Message = "This endpoint is protected by JWT."
            });
        });

        app.MapGet("/api/profile/claims", [Authorize] (ClaimsPrincipal principal) =>
        {
            var claims = principal.Claims
                .Select(claim => new { claim.Type, claim.Value })
                .OrderBy(item => item.Type)
                .ToList();

            return Results.Ok(claims);
        });

        app.MapGet("/api/profile/permissions", [Authorize] (ClaimsPrincipal principal) =>
        {
            var permissions = principal.Claims
                .Where(claim => claim.Type == "permission")
                .Select(claim => claim.Value)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(value => value)
                .ToList();

            return Results.Ok(new
            {
                Username = principal.FindFirstValue(ClaimTypes.Name) ?? "unknown",
                Permissions = permissions
            });
        });

        app.MapGet("/api/profile/permissions/{permission}", [Authorize] (
            ClaimsPrincipal principal,
            string permission) =>
        {
            if (string.IsNullOrWhiteSpace(permission))
            {
                return Results.BadRequest(new
                {
                    title = "Invalid permission",
                    status = 400,
                    detail = "Permission is required."
                });
            }

            var normalizedPermission = permission.Trim();
            var hasPermission = principal.Claims.Any(claim =>
                claim.Type == "permission" &&
                claim.Value.Equals(normalizedPermission, StringComparison.OrdinalIgnoreCase));

            return Results.Ok(new
            {
                Username = principal.FindFirstValue(ClaimTypes.Name) ?? "unknown",
                Permission = normalizedPermission,
                HasPermission = hasPermission
            });
        });

        app.MapGet("/api/admin/reports", [Authorize(Roles = "Admin")] () =>
        {
            return Results.Ok(new
            {
                Message = "Confidential admin report.",
                GeneratedAtUtc = DateTimeOffset.UtcNow
            });
        });

        app.MapGet("/api/admin/users", [Authorize(Roles = "Admin")] (DemoUserStore userStore) =>
        {
            return Results.Ok(userStore.GetUsers());
        });

        return app;
    }
}
