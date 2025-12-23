namespace Application.Contracts.Admin;

public record UpdateUserRequest
(
    string Email,
    string UserFullName,
    string UserAddress,
    string Roles
    );
