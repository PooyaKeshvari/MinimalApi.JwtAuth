using MinimalApi.JwtAuth.Application.Contracts;

namespace MinimalApi.JwtAuth.Application.Services;

public sealed class DemoUserStore
{
    private static readonly List<DemoUser> Users =
    [
        new DemoUser("pooya", "pass123", "Admin", "Pooya A."),
        new DemoUser("dev", "pass123", "Developer", "Dev User")
    ];

    public DemoUser? Validate(string username, string password)
    {
        return Users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
            && u.Password == password);
    }

    public IReadOnlyList<UserSummaryResponse> GetUsers()
    {
        return Users
            .Select(user => new UserSummaryResponse
            {
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role
            })
            .OrderBy(user => user.Username)
            .ToList();
    }
}

public sealed record DemoUser(
    string Username,
    string Password,
    string Role,
    string FullName);
