using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace QueflityMVC.Web.Setup.Database;

internal static class DbExtensions
{
    internal static void ApplyPendingMigrations<TContext>(this WebApplication webApplication) where TContext : DbContext
    {
        using var scope = webApplication.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        context.Database.EnsureCreated();
    }
}