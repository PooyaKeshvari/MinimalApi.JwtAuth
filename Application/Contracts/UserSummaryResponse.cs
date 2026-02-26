namespace MinimalApi.JwtAuth.Application.Contracts;

public sealed class UserSummaryResponse
{
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
