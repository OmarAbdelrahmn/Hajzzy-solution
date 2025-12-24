using Application.Abstraction;
using Application.Abstraction.Errors;
using Application.Contracts.Admin;
using Application.Roles;
using Domain;
using Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin;

public class AdminService(UserManager<ApplicationUser> manager, ApplicationDbcontext dbcontext, IRoleService roleService) : IAdminService
{
    private readonly IRoleService roleService = roleService;

    public async Task<Result<UserResponse>> AddUserAsync(CreateUserRequest request)
    {
        var EmailIsexist = await manager.Users.AnyAsync(c => c.Email == request.Email);

        if (EmailIsexist)
            return Result.Failure<UserResponse>(UserErrors.EmailAlreadyExist);

        var allowedroles = await roleService.GetRolesAsync();

        if (!allowedroles.Value.Any(r => r.Name == request.Role))
            return Result.Failure<UserResponse>(RolesErrors.InvalidRoles);

        var user = request.Adapt<ApplicationUser>();
        user.UserName = request.Email;
        user.EmailConfirmed = true;

        var result = await manager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await manager.AddToRoleAsync(user, request.Role);

            var response = (user, request.Role).Adapt<UserResponse>();

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

    public async Task<Result<IEnumerable<UserResponse>>> GetAllUsers()
    {
        var users = await (from u in dbcontext.Users
               join ur in dbcontext.UserRoles
               on u.Id equals ur.UserId
               join r in dbcontext.Roles
               on ur.RoleId equals r.Id into roles
               select new
               {
                   u.Id,
                   u.FullName,
                   u.Address,
                   u.Email,
                   u.IsDisable,
                   role = roles.Select(r => r.Name!).FirstOrDefault()
               })
                  .GroupBy(x => new { x.Id, x.FullName, x.Address, x.Email, x.IsDisable })
                  .Select(c => new UserResponse(
                      c.Key.Id,
                      c.Key.FullName,
                      c.Key.Address,
                      c.Key.Email,
                      c.Key.IsDisable,
                      c.Select(x => x.role).FirstOrDefault()!
                      ))
                  .ToListAsync();

        if(users.Count() == 0)
            return Result.Failure<IEnumerable<UserResponse>>(UserErrors.UserNotFound);

            return Result.Success<IEnumerable<UserResponse>>(users);
        
    }

    public async Task<Result<UserResponse>> GetUser2Async(string UserName)
    {
        if (await manager.FindByNameAsync(UserName) is not { } user)
            return Result.Failure<UserResponse>(UserErrors.UserNotFound);

        var userroles = await manager.GetRolesAsync(user);

        var response = (user, userroles).Adapt<UserResponse>();

        return Result.Success(response);
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
            return Result.Failure(UserErrors.EmailAlreadyExist);

        var allowedroles = await roleService.GetRolesAsync();

        if (!allowedroles.Value.Any(r => r.Name == request.Role))
            return Result.Failure(RolesErrors.InvalidRoles);

        user = request.Adapt(user);

        user.UserName = request.Email;
        user.NormalizedUserName = request.Email.ToUpper();
        if(request.Password != null)
        {
            await manager.RemovePasswordAsync(user);
            var newpass = await manager.AddPasswordAsync(user, request.Password);

            if (!newpass.Succeeded)
            {
                return Result.Failure(new Error("password failed", "password failed to updaude", 404));
            }
        }
        var result = await manager.UpdateAsync(user);

        if (result.Succeeded)
        {

            await dbcontext.UserRoles
                .Where(c => c.UserId == UserId)
                .ExecuteDeleteAsync();

            await manager.AddToRoleAsync(user, request.Role);

            return Result.Success();
        }

        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

    }

}
