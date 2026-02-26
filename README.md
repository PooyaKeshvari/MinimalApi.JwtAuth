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
- `POST /api/auth/token`
- `GET /api/profile/me`
- `GET /api/admin/reports`

## Sample Login Payload
```json
{
  "username": "pooya",
  "password": "pass123"
}
```

## Run
```bash
dotnet run --project src/MinimalApi-JwtAuth/MinimalApi.JwtAuth.csproj
```

## Key Files
- `Program.cs`
- `Services/JwtTokenService.cs`
- `Services/JwtOptions.cs`
- `Services/DemoUserStore.cs`

## Interview Talking Points
- Why key length and option validation matter.
- Difference between authentication and authorization.
- Claims extraction and role checks in minimal APIs.
