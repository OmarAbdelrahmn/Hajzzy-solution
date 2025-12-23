using Application.Abstraction;
using Application.Contracts.Roles;
using Medical_E_Commerce.Contracts.Roles;

namespace Application.Roles;

public interface IRoleService
{
    Task<Result<IEnumerable<RolesResponse>>> GetRolesAsync(bool? IncludeDisable = true);
    Task<Result<RoleDetailsResponse>> GetRoleByIdAsync(string RollId);
    Task<Result> ToggleStatusAsync(string RoleName);
    Task<Result> UpdateRoleAsync(RoleRequest request);
    Task<Result> addroleAsync(RoleRequest request);
    Task<Result> UpdateRoleAsync(string Id, RoleRequest request);
}
