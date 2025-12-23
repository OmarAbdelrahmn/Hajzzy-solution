namespace Application.Contracts.User;

public record UserProfileResponse
(
    string Email,
    string UserFullName,
    string UserAddress
    );
