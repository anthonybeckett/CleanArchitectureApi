using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Application.Identity.DTO;
using CleanArchitectureApi.Application.Identity.Users.DTO;

namespace CleanArchitectureApi.Application.Identity.Users.Commands;

public record RegisterUserCommand(RegisterUserDto Dto) : ICommand<RegisterUserResponse>;