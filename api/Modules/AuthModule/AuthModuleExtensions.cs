using System.Text;
using api.Extensions;
using api.Modules.AuthModule.Domain;
using api.Modules.AuthModule.Infrastructure;
using api.Modules.AuthModule.Jwt;
using api.Modules.AuthModule.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace api.Modules.AuthModule
{
    public static class AuthModuleExtensions
    {
        public static IServiceCollection AddAuthModule(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>()!;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtOptions:Issuer"],
                    ValidAudience = configuration["JwtOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddTransient<RoleMiddleware>();
            services.AddHostedService<AdminSeed>();

            return services;
        }
    }
}
