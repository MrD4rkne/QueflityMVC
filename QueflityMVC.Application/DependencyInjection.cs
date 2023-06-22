using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.Validators;
using QueflityMVC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // FluentValidation
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<ItemCategoryValidator>();

            // Services
            services.AddTransient<IItemCategoryService, ItemCategoryService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IIngredientService, IngredientService>();

            return services;
        }
    }
}
