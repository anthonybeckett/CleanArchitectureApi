using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Application.Abstractions.TokenProvider;
using CleanArchitectureApi.Application.Identity.Users.DTO;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Identity.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureApi.Application.Identity.Users.Commands;

internal sealed class RefreshTokenCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService) : ICommandHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principle = tokenService.GetPrincipalFromExpiredToken(request.Dto.AccessToken);
        
        var email = principle.FindFirstValue(ClaimTypes.Email);

        if (email == null)
        {
            return Result<RefreshTokenResponse>.Failure(HttpStatusCode.BadRequest, new Error
            {
                ErrorCode = "RefreshTokenFail.Error",
                ErrorMessage = ["Could not find email from token"],
            });
        }
        
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return Result<RefreshTokenResponse>.Failure(HttpStatusCode.BadRequest, new Error
            {
                ErrorCode = "RefreshTokenFail.Error",
                ErrorMessage = ["User does not exist"],
            });
        }
        
        var roles = await userManager.GetRolesAsync(user);
        
        if (roles == null || roles.Count == 0)
        {
            return Result<RefreshTokenResponse>.Failure(HttpStatusCode.BadRequest, new Error
            {
                ErrorCode = "RefreshTokenFail.Error",
                ErrorMessage = ["User does not have any roles assigned"],
            });
        }
        
        var newRefreshToken = tokenService.GenerateRefreshToken();

        var refreshTokenExpireDate = tokenService.GetRefreshTokenExpirationDate();
        
        user.UpdateRefreshToken(request.Dto.RefreshToken, newRefreshToken, refreshTokenExpireDate);

        var newAccessToken = await tokenService.CreateTokenAsync(user, roles);

        await userManager.UpdateAsync(user);

        var response = new RefreshTokenResponse
        {
            NewAccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            NewRefreshToken = newRefreshToken,
            RefreshTokenExpireDate = refreshTokenExpireDate,
        };
        
        return Result<RefreshTokenResponse>.Success(response, HttpStatusCode.OK);
    }
}