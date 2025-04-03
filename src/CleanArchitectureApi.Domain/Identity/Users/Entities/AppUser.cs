using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Identity.Users.Events;
using CleanArchitectureApi.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureApi.Domain.Identity.Users.Entities;

public class AppUser : IdentityUser<Guid>, IDomainEventRaiser
{
    private readonly List<IDomainEvent> _domainEvents = [];

    private AppUser(Guid id, string fullname, string email, string username) : base(username)
    {
        Id = id;
        Fullname = fullname;
        Email = email;
        SecurityStamp = Guid.NewGuid().ToString();
    }

    private AppUser()
    {
    }

    public string Fullname { get; private set; }

    public string? RefreshToken { get; private set; }

    public DateTime? RefreshTokenExpireDate { get; private set; }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public static async Task<AppUser> Create(string fullname, string email, string adminKey,
        UserManager<AppUser> userManager)
    {
        var userAlreadyExists = await userManager.FindByEmailAsync(email) != null;

        if (userAlreadyExists) throw new UserAlreadyExistsException(["User already exists"]);

        var user = new AppUser(Guid.NewGuid(), fullname, email, email);

        user.RaiseDomainEvent(new UserRegisteredDomainEvent(user.Id, adminKey));

        return user;
    }

    public void AddRefreshToken(string refreshToken, DateTime refreshTokenExpireDate)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpireDate = refreshTokenExpireDate;
    }

    public void UpdateRefreshToken(string refreshToken, DateTime refreshTokenExpireDate)
    {
        if (!string.IsNullOrEmpty(RefreshToken) && !RefreshToken.Equals(refreshToken))
            throw new InvalidTokenException(["Invalid refresh token"]);

        if (RefreshTokenExpireDate.HasValue && RefreshTokenExpireDate < DateTime.Now)
            throw new InvalidTokenException(["Refresh token expired"]);

        RefreshToken = refreshToken;
        RefreshTokenExpireDate = refreshTokenExpireDate;
    }

    public void RevokeUser()
    {
        RefreshToken = null;
        RefreshTokenExpireDate = null;
    }
}