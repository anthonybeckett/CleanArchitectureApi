using System.Net;
using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Identity.Roles.Entities;
using CleanArchitectureApi.Domain.Identity.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureApi.Application.Identity.Users.Commands;

internal sealed class RevokeAllUsersCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    : ICommandHandler<RevokeAllusersCommand, NoContentDto>
{
    public async Task<Result<NoContentDto>> Handle(RevokeAllusersCommand request, CancellationToken cancellationToken)
    {
        IList<AppUser> users;

        if (string.IsNullOrEmpty(request.Dto.Role))
        {
            users = await userManager.Users.ToListAsync(cancellationToken);
        }
        else
        {
            var role = await roleManager.FindByNameAsync(request.Dto.Role);

            if (role == null)
            {
                return Result<NoContentDto>.Failure(HttpStatusCode.BadRequest, new Error
                {
                    ErrorCode = "RevokeAllUser.Error",
                    ErrorMessage = ["Role not found"],
                });
            }

            users = await userManager.GetUsersInRoleAsync(request.Dto.Role);
        }
        
        foreach (var user in users)
        {
            user.RevokeUser();
                
            await userManager.UpdateAsync(user);
        }

        return Result<NoContentDto>.Success(HttpStatusCode.NoContent);
    }
}