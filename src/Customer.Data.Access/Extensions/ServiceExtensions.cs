// -------------------------------------------------------------------------------------
//  <copyright file="ServiceExtensions.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Data.Access.Extensions;

using Customer.Data.Access.Repositories.Implementations;
using Customer.Data.Access.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for the services.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Adds the DB Context configuration to the pipeline.
    /// </summary>
    /// <param name="services">The service collections.</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    /// <returns>An instance of <see cref="IConfiguration"/>.</returns>
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        //// services.AddDbContext<CustomerContext>(options =>
        ////        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<CustomerContext>(options =>
                options.UseInMemoryDatabase(databaseName: "Mock DB"));

        return services;
    }

    /// <summary>
    /// Adds the respositories dependency injection into the pipeline.
    /// </summary>
    /// <param name="services">The service collections.</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}