﻿using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.Validators;

namespace QueflityMVC.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // FluentValidation
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<KitValidator>();

        // Services
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IItemService, ItemService>();
        services.AddTransient<IComponentService, ComponentService>();
        services.AddTransient<IKitService, KitService>();
        services.AddTransient<IFileService, FileService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IPurchasableEntityService, PurchasableEntityService>();
        services.AddTransient<IMessageService, MessageService>();

        return services;
    }
}