using System.Net;
using AutoMapper;
using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Application.Identity.Users.DTO;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Identity.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureApi.Application.Identity.Users.Commands;

internal sealed class RegisterUserCommandHandler(
    UserManager<AppUser> userManager,
    IMapper mapper) : ICommandHandler<RegisterUserCommand, RegisterUserResponse>
{
    public async Task<Result<RegisterUserResponse>> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await AppUser.Create(request.Dto.Fullname, request.Dto.Email, request.Dto.AdminKey, userManager);

        var result = await userManager.CreateAsync(user, request.Dto.Password);

        if (!result.Succeeded)
        {
            return Result<RegisterUserResponse>.Failure(HttpStatusCode.BadRequest, new Error
            {
                ErrorCode = "RegisterFailed.Error",
                ErrorMessage = result.Errors.Select(e => e.Description).ToList()
            });
        }

        var response = mapper.Map<RegisterUserResponse>(user);

        return Result<RegisterUserResponse>.Success(response, HttpStatusCode.Created);
    }
}