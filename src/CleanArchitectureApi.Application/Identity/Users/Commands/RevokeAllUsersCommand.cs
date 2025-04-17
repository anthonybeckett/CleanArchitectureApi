using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Application.Identity.DTO;

namespace CleanArchitectureApi.Application.Identity.Users.Commands;

public record RevokeAllusersCommand(RevokeAllUsersDto Dto) : ICommand<NoContentDto>;