using Microsoft.Extensions.DependencyInjection;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Infrastructure.Repositories;

namespace QueflityMVC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IIngredientRepository, IngredientRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IItemSetRepository, ItemSetRepository>();

            return services;
        }
    }
}
