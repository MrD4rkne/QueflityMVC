using Microsoft.Extensions.DependencyInjection;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Infrastructure.Repositories;

namespace QueflityMVC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IIngredientRepository, IngredientRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IKitRepository, KitRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
