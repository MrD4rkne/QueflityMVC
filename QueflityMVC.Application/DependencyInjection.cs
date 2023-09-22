using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.Validators;
using System.Reflection;

namespace QueflityMVC.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // FluentValidation
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CategoryValidator>();

            // Services
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IIngredientService, IngredientService>();
            services.AddTransient<IKitService, KitService>();
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<IFileService, FileService>();

            return services;
        }
    }
}
