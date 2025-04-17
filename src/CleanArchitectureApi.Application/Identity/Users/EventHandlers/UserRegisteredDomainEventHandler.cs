using CleanArchitectureApi.Application.Abstractions.Emails;
using CleanArchitectureApi.Domain.Identity.Roles.Entities;
using CleanArchitectureApi.Domain.Identity.Users.Entities;
using CleanArchitectureApi.Domain.Identity.Users.Events;
using CleanArchitectureApi.Domain.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureApi.Application.Identity.Users.EventHandlers;

internal sealed class UserRegisteredDomainEventHandler(UserManager<AppUser> userManager, IEmailService emailService)
    : INotificationHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(notification.UserId.ToString());

        if (user == null)
        {
            return;
        }

        if (string.IsNullOrEmpty(notification.AdminKey))
        {
            await userManager.AddToRoleAsync(user, AppRole.User.Name);
        }
        else
        {
            if (!notification.AdminKey.Equals(AppRole.ADMIN_KEY))
            {
                throw new AdminKeyNotMatchException(["Admin key not matched"]);
            }

            await userManager.AddToRoleAsync(user, AppRole.Admin.Name);
        }
    }
}