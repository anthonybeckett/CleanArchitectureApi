using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Attributes;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureApi.Domain.Identity.Roles.Entities;

public class AppRole : IdentityRole<Guid>, IAutoseedData
{
    public const string ADMIN_KEY = "123456";

    private AppRole(
        Guid id,
        string concurrencyStamp,
        string name)
    {
        Id = id;
        ConcurrencyStamp = concurrencyStamp;
        Name = name;
        NormalizedName = name.ToUpper();
    }

    public AppRole()
    {
    }

    [AutoSeedData]
    public static AppRole User => new(
        Guid.Parse("CF005204-6AAC-4AD4-8EA3-346C5A665CB1"),
        "3e8a2c66-9e8b-4fc6-a8cf-14bdf6fcb2f7",
        "User"
        );

    [AutoSeedData]
    public static AppRole Admin => new(
        Guid.Parse("0A83E49B-7630-4CBC-9047-2F6E55301CC2"),
        "d1f4bc78-5d4a-4eb9-9c6d-c5cdb9f96a76",
        "Admin");
}