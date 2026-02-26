namespace MinimalApi.JwtAuth.Application.Services;

public interface ITokenService
{
    string GenerateToken(DemoUser user);
}
