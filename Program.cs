using Apps.Data;
using Apps.Options;
using Apps.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
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
var app = builder.Build();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
// Map the conventional controller route
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
