using System.Net;
using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Identity.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureApi.Application.Identity.Users.Commands;

internal sealed class RevokeUserCommandHandler(UserManager<AppUser> userManager) : ICommandHandler<RevokeUserCommand, NoContentDto>
{
    public async Task<Result<NoContentDto>> Handle(RevokeUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Dto.Email);

        if (user  == null)
        {
            return Result<NoContentDto>.Failure(HttpStatusCode.BadRequest, new Error
            {
                ErrorCode = "RevokeUser.Error",
                ErrorMessage = ["User not found"],
            });
        }
        
        user.RevokeUser();
        
        await userManager.UpdateAsync(user);
        
        return Result<NoContentDto>.Success(HttpStatusCode.NoContent);
    }
}