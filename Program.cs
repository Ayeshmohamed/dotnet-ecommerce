using Apps.Data;
using Apps.Middlewares;
using Apps.Options;
using Apps.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;
var builder = WebApplication.CreateBuilder(args);
var jwtKey = builder.Configuration["Jwt:Key"]!;

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            ),

            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();
builder.Services.AddDbContext<DatabaseContext>(
    (serviceProvider, dbContextOptionsBuilder) =>
        {
            var databaseOptions = serviceProvider
               .GetRequiredService<IOptions<DatabaseOptions>>()!
               .Value;

            dbContextOptionsBuilder.UseSqlServer(
                databaseOptions.ConnectionString,
                sqlServerAction =>
                {
                    //sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);

                    sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
                });

            dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);

            // Enable only during development
            if (builder.Environment.IsDevelopment())
            {
                dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            }
        }
    );

builder.Services.AddServices();
builder.Services.AddRepositories();
// Register API versioning services.
builder.Services
    .AddApiVersioning(options =>
    {
        // Use version 1.0 as the default API version.
        options.DefaultApiVersion = new ApiVersion(1, 0);

        // Treat requests without an explicit version as v1.
        options.AssumeDefaultVersionWhenUnspecified = true;

        // Include supported/deprecated version information in responses.
        options.ReportApiVersions = true;

        // Read the version from the URL path.
        options.ApiVersionReader =
            new UrlSegmentApiVersionReader();
    })
    // Connect API versioning to MVC controllers.
    .AddMvc();

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter =
        PartitionedRateLimiter.Create<HttpContext, string>(
            httpContext =>
            {
                var ipAddress =
                    httpContext.Connection.RemoteIpAddress?.ToString()
                    ?? "unknown";

                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: ipAddress,
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 60,
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0,
                        AutoReplenishment = true
                    });
            });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

builder.Services.AddHealthChecks()

    // Check whether the configured EF Core database is accessible.
    .AddDbContextCheck<DatabaseContext>();
var app = builder.Build();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
// Map the conventional controller route
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHealthChecks("/health");
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
