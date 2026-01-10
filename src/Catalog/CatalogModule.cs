using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<CatalogDbContext>(options => options.UseNpgsql(connectionString));
        
        return services;
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        InitializeDataBaseAsync(app).GetAwaiter().GetResult();
        return app;
    }

    private static async Task InitializeDataBaseAsync(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        await context.Database.MigrateAsync();
    }
}