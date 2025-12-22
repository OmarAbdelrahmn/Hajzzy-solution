

using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; } = string.Empty;

    public string? Address { get; set; } = string.Empty;

    public bool IsDisable { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; } = [];

}
