namespace Apps.Services
{
    public static class DtosRegisteration
    {
        public static IServiceCollection AddDtos(
       this IServiceCollection services)
        {
            services.AddScoped<ProductService>();
            services.AddScoped<CategoryService>();
            return services;
        }
    }
}