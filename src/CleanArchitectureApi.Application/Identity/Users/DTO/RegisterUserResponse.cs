using AutoMapper;
using CleanArchitectureApi.Application.Identity.DTO;
using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Application.Identity.Users.DTO;

public class RegisterUserResponse : IResult
{
    public Guid Id { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
}

public class RegisterUserMapper : Profile
{
    public RegisterUserMapper()
        => CreateMap<RegisterUserDto, RegisterUserResponse>();
}