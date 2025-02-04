﻿using System.Reflection;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Web.Application.Interfaces;

namespace Persistence;

public static class DependencyInjection
{
    private static string _postgreConnectionString = Environment.GetEnvironmentVariable("POSTGRESQL_CONNECTION_STRING");
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<DbContext>(cfg =>
        {
            cfg.UseNpgsql(_postgreConnectionString);
            cfg.EnableSensitiveDataLogging(true);
        });

        services.AddIdentityCore<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<DbContext>();
        services.AddScoped<IDbContext>(x => x.GetService<DbContext>());

        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<DbContext>()
            .AddDefaultTokenProviders();



        return services;
    }
}
