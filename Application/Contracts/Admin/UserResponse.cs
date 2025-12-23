namespace Application.Contracts.Admin;

public record UserResponse
(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    bool IsDisable,
    string Role
    );
