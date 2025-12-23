using Application.Abstraction;
using Application.Abstraction.Errors;
using Application.Admin;
using Application.Contracts.Users;
using Application.Roles;
using Domain;
using Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Medical_E_Commerce.Service.Admin;

public class AdminService(UserManager<ApplicationUser> manager, ApplicationDbcontext dbcontext, IRoleService roleService) : IAdminService
{
    private readonly IRoleService roleService = roleService;

    public async Task<Result<UserResponse>> AddUserAsync(CreateUserRequest request)
    {
        var EmailIsexist = await manager.Users.AnyAsync(c => c.Email == request.Email);

        if (EmailIsexist)
            return Result.Failure<UserResponse>(UserErrors.EmailIsExcists);

        var allowedroles = await roleService.GetRolesAsync();

        if (request.Roles.Except(allowedroles.Value.Select(c => c.Name)).Any())
            return Result.Failure<UserResponse>(RolesErrors.InvalidRoles);

        var user = request.Adapt<ApplicationUser>();
        user.UserName = request.Email;
        user.EmailConfirmed = true;

        var result = await manager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await manager.AddToRolesAsync(user, request.Roles);

            var response = (user, request.Roles).Adapt<UserResponse>();

            return Result.Success(response);
        }

        var error = result.Errors.First();
        return Result.Failure<UserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }

    public async Task<Result> EndLockOutAsync(string UserId)
    {
        if (await manager.FindByIdAsync(UserId) is not { } user)
            return Result.Failure(UserErrors.UserNotFound);

        var result = await manager.SetLockoutEndDateAsync(user, null);

        if (result.Succeeded)
            return Result.Success();

        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }

    public async Task<IEnumerable<UserResponse>> GetAllUsers() =>
        await (from u in dbcontext.Users
               join ur in dbcontext.UserRoles
               on u.Id equals ur.UserId
               join r in dbcontext.Roles
               on ur.RoleId equals r.Id into roles
               select new
               {
                   u.Id,
                   u.UserFullName,
                   u.UserAddress,
                   u.Email,
                   u.IsDisable,
                   roles = roles.Select(r => r.Name!).ToList()
               })
                  .GroupBy(x => new { x.Id, x.UserFullName, x.UserAddress, x.Email, x.IsDisable })
                  .Select(c => new UserResponse(
                      c.Key.Id,
                      c.Key.UserFullName,
                      c.Key.UserAddress,
                      c.Key.Email,
                      c.Key.IsDisable,
                      c.SelectMany(x => x.roles)
                      ))
                  .ToListAsync();

    public Task<Result<UserResponses>> GetUser2Async(string UserName)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<UserResponse>> GetUserAsync(string Id)
    {
        if (await manager.FindByIdAsync(Id) is not { } user)
            return Result.Failure<UserResponse>(UserErrors.UserNotFound);

        var userroles = await manager.GetRolesAsync(user);

        var response = (user, userroles).Adapt<UserResponse>();

        return Result.Success(response);
    }

    public async Task<Result> ToggleStatusAsync(string UserId)
    {
        if (await manager.FindByIdAsync(UserId) is not { } user)
            return Result.Failure(UserErrors.UserNotFound);

        user.IsDisable = !user.IsDisable;

        var result = await manager.UpdateAsync(user);
        if (result.Succeeded)
            return Result.Success();

        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }

    public async Task<Result> UpdateUserAsync(string UserId, UpdateUserRequest request)
    {

        if (await manager.FindByIdAsync(UserId) is not { } user)
            return Result.Failure(UserErrors.UserNotFound);

        var duplicatedEmail = await manager.Users.AnyAsync(c => c.Email == request.Email && c.Id != UserId);

        if (duplicatedEmail)
            return Result.Failure(UserErrors.EmailIsExcists);

        var allowedroles = await roleService.GetRolesAsync();

        if (request.Roles.Except(allowedroles.Value.Select(c => c.Name)).Any())
            return Result.Failure(RolesErrors.InvalidRoles);

        user = request.Adapt(user);

        user.UserName = request.Email;
        user.NormalizedUserName = request.Email.ToUpper();

        var result = await manager.UpdateAsync(user);

        if (result.Succeeded)
        {

            await dbcontext.UserRoles
                .Where(c => c.UserId == UserId)
                .ExecuteDeleteAsync();

            await manager.AddToRolesAsync(user, request.Roles);

            return Result.Success();
        }

        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

    }

    Task<IEnumerable<UserResponses>> IAdminService.GetAllUsers()
    {
        throw new NotImplementedException();
    }

    Task<Result<UserResponses>> IAdminService.GetUserAsync(string Id)
    {
        throw new NotImplementedException();
    }
}
