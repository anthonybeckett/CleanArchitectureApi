using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Application.Identity.DTO;

namespace CleanArchitectureApi.Application.Identity.Users.Commands;

public record RevokeUserCommand(RevokeUserDto Dto) : ICommand<NoContentDto>;