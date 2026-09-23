
namespace Apps.Services
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(
       this IServiceCollection services)
        {
            services.AddScoped<ProductService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<UserService>();
            services.AddScoped<JwtService>();

            return services;
        }
    }
}