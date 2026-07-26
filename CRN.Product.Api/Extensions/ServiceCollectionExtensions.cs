using CRN.Product.Application.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CRN.Product.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings =
                configuration
                .GetSection("JwtSettings")
                .Get<JwtSettings>();

            services.Configure<JwtSettings>(
                configuration.GetSection("JwtSettings"));

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;

                    options.SaveToken = true;

                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,

                            ValidateAudience = true,

                            ValidateLifetime = true,

                            ValidateIssuerSigningKey = true,

                            ValidIssuer = jwtSettings!.Issuer,

                            ValidAudience = jwtSettings.Audience,

                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                        };
                });

            return services;
        }
    }
}
