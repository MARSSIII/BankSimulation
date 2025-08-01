using Contracts.ApplicationContexts;
using Contracts.Builders;
using Contracts.Service;
using Lab5.Application.ApplicationContexts;
using Lab5.Application.Builders;
using Lab5.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lab5.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<ApplicationContext>();
        collection.AddScoped<IApplicationContext>(
            p => p.GetRequiredService<ApplicationContext>());

        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IAccountService, AccountService>();

        collection.AddScoped<IAccountBuilder, AccountBuilder>();
        collection.AddScoped<IUserBuilder, UserBuilder>();

        return collection;
    }
}