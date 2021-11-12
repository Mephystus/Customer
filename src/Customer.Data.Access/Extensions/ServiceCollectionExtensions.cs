// -------------------------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Data.Access.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Implementations;
using Repositories.Interfaces;

/// <summary>
/// Provides extension methods for the services.
/// </summary>
public static class ServiceCollectionExtensions
{
    #region Public Methods

    /// <summary>
    /// Adds the DB Context configuration into the DI pipeline.
    /// </summary>
    /// <param name="services">The service collections.</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CustomerContext>(options =>
                options.UseInMemoryDatabase(databaseName: "Mock DB"));

        return services;
    }

    /// <summary>
    /// Adds the repositories dependency injection into the DI pipeline.
    /// </summary>
    /// <param name="services">The service collections.</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }

    #endregion Public Methods
}