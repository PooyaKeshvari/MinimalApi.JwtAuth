# Minimal API JWT Auth

Minimal API sample that issues JWTs and protects endpoints with role-based authorization.

## Features
- JWT token generation endpoint.
- Protected endpoint with `[Authorize]`.
- Admin-only endpoint with `[Authorize(Roles = "Admin")]`.
- Startup-time configuration validation for JWT options.
- Swagger configured with Bearer auth scheme.
- Health endpoint: `GET /health`.

## Endpoints
- `POST /api/auth/token` — exchange credentials for a JWT.
- `GET /api/profile/me` — requires a valid JWT.
- `GET /api/profile/claims` — lists all claims in the token.
- `GET /api/profile/permissions` — lists permission claims.
- `GET /api/profile/permissions/{permission}` — checks a single permission.
- `GET /api/admin/reports` — requires the `Admin` role.
- `GET /api/admin/users` — requires the `Admin` role.
- `GET /health` — health check.

## Sample Login Payload
```json
{
  "username": "pooya",
  "password": "pass123"
}
```

Demo users: `pooya` / `pass123` (Admin) and `dev` / `pass123` (Developer).

## Run
```bash
dotnet run --project MinimalApi.JwtAuth.csproj
```

The API launches with Swagger UI at `/swagger` in Development.

## Key Files
- `Program.cs`
- `Application/Services/JwtTokenService.cs`
- `Application/Services/JwtOptions.cs`
- `Application/Services/DemoUserStore.cs`

## Interview Talking Points
- Why key length and option validation matter.
- Difference between authentication and authorization.
- Claims extraction and role checks in minimal APIs.
