﻿// -------------------------------------------------------------------------------------
//  <copyright file="CustomerMappingProfile.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------


namespace Customer.Services.Mappers;

using AutoMapper;
using Customer.Data.Schema;

/// <summary>
/// Provides the mapping configuration for the customer DTOs.
/// </summary>
public class CustomerMappingProfile : Profile
{
    /// <summary>
    /// Initialises a new instance of the <see cref="CustomerMappingProfile"/> class.
    /// </summary>
    public CustomerMappingProfile()
    {
        CreateMap<ExternalServices.Dto.CustomerRiskResponse, Models.CustomerRiskResponse>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Description));

        CreateMap<Customer, Models.CustomerResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));

        CreateMap<Models.CustomerRequest, Customer>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id));
    }
}
