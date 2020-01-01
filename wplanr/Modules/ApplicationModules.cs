using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using wplanr.Core.ConfigurationModels;
using System.Text;


namespace wplanr.Modules
{
    public static class ApplicationModules
    {

        public static void SetupJwtMechanism(IServiceCollection services, JwtAuthenticationSettings jwtSettings)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
            {
                x.Audience = jwtSettings.Audience;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = jwtSettings.ValidateSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey)),
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ClockSkew = System.TimeSpan.Zero
                };
            }).AddJwtBearer("Temp", x =>
            {
                x.Audience = jwtSettings.Audience;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = jwtSettings.ValidateSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.TempSigningKey)),
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ClockSkew = System.TimeSpan.Zero
                };
            });
        }


        public static void ConfigureLogging(IServiceCollection services, string instrumentationKey)
        {

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole(x => x.IncludeScopes = true);
            });

        }

    }
}
