namespace Application.Contracts.User;

public record ChangePasswordRequest
(
    string CurrentPassword,
    string NewPassord
    );
