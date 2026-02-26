namespace MinimalApi.JwtAuth.Application.Services;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = "ResumeProjects";
    public string Audience { get; set; } = "ResumeProjects.Client";
    public string Key { get; set; } = "change-me";
    public int ExpirationMinutes { get; set; } = 60;
}
