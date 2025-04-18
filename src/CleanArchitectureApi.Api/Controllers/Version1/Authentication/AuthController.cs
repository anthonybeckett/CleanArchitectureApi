using CleanArchitectureApi.Application.Identity.DTO;
using CleanArchitectureApi.Application.Identity.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureApi.Api.Controllers.Version1.Authentication;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(ISender sender) : BaseController
{
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUserAsync(RegisterUserDto request,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new RegisterUserCommand(request), cancellationToken);

        return CreateResult(response);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginUserAsync(LoginUserDto request,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new LoginUserCommand(request), cancellationToken);

        return CreateResult(response);
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshTokenAsync(RefreshTokenDto request,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new RefreshTokenCommand(request), cancellationToken);

        return CreateResult(response);
    }

    [Authorize(Roles = "Admin,User")]
    [HttpPost("RevokeUser")]
    public async Task<IActionResult> RevokeUserAsync(RevokeUserDto request,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new RevokeUserCommand(request), cancellationToken);

        return CreateResult(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("RevokeAll")]
    public async Task<IActionResult> RevokeAllAsync(RevokeAllUsersDto request,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new RevokeAllusersCommand(request), cancellationToken);

        return CreateResult(response);
    }
}