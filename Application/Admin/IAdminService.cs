using Application.Abstraction;


namespace Application.Admin;

public interface IAdminService
{
    Task<IEnumerable<UserResponses>> GetAllUsers();
    Task<Result<UserResponses>> GetUserAsync(string Id);
    Task<Result<UserResponses>> GetUser2Async(string UserName);
    Task<Result<UserResponse>> AddUserAsync(CreateUserRequest request);
    Task<Result> UpdateUserAsync(string UserId, UpdateUserRequest request);
    Task<Result> ToggleStatusAsync(string UserId);
    Task<Result> EndLockOutAsync(string UserId);
}
