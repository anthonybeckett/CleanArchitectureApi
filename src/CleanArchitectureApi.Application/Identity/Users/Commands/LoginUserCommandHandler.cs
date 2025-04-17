using System.IdentityModel.Tokens.Jwt;
using System.Net;
using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Application.Abstractions.TokenProvider;
using CleanArchitectureApi.Application.Identity.Users.DTO;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Identity.Roles.Entities;
using CleanArchitectureApi.Domain.Identity.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureApi.Application.Identity.Users.Commands;

internal sealed class LoginUserCommandHandler(
    UserManager<AppUser> userManager,
    ITokenService tokenService) : ICommandHandler<LoginUserCommand, LoginUserResponse>
{
    public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Dto.Email);

        if (user == null)
        {
            return Result<LoginUserResponse>.Failure(HttpStatusCode.Unauthorized, new Error
            {
                ErrorCode = "LoginFailed.Error",
                ErrorMessage = ["Username cannot be found"]
            });
        }

        var isValidPassword = await userManager.CheckPasswordAsync(user, request.Dto.Password);

        if (!isValidPassword)
        {
            return Result<LoginUserResponse>.Failure(HttpStatusCode.Unauthorized, new Error
            {
                ErrorCode = "LoginFailed.Error",
                ErrorMessage = ["Password is incorrect"]
            });
        }

        var roles = await userManager.GetRolesAsync(user);

        var accessToken = await tokenService.CreateTokenAsync(user, roles);

        var refreshToken = tokenService.GenerateRefreshToken();
        
        user.AddRefreshToken(refreshToken, tokenService.GetRefreshTokenExpirationDate());
        
        await userManager.UpdateAsync(user);

        await userManager.UpdateSecurityStampAsync(user);
        
        var token = new JwtSecurityTokenHandler().WriteToken(accessToken);

        var response = new LoginUserResponse
        {
            AccessToken = token,
            RefreshToken = refreshToken,
            ExpireDate = accessToken.ValidTo
        };
        
        return Result<LoginUserResponse>.Success(response, HttpStatusCode.OK);
    }
}