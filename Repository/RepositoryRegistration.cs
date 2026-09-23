
using Apps.Repository;

public static class RepositoryRegistration
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<ProductRepository>();
        services.AddScoped<ProductImageRepository>();
        services.AddScoped<CategoryRepository>();
        services.AddScoped<FilesUpload>();
        services.AddScoped<UserRepository>();
        return services;
    }
}