using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CloudFabric.Auth
{
    public static class AzureAdServiceCollectionExtensions
    {
        public static AuthenticationBuilder AddAzureAdBearer(this AuthenticationBuilder builder)
            => builder.AddAzureAdBearer(_ => { });

        public static AuthenticationBuilder AddAzureAdBearer(this AuthenticationBuilder builder, Action<AzureAdOptions> configureOptions)
        {
            builder.Services.Configure(configureOptions);
            builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureAzureOptions>();
            builder.AddJwtBearer();
            return builder;
        }

        private class ConfigureAzureOptions : IConfigureNamedOptions<JwtBearerOptions>
        {
            private readonly AzureAdOptions _azureOptions;

            public ConfigureAzureOptions(IOptions<AzureAdOptions> azureOptions)
            {
                _azureOptions = azureOptions.Value;
            }

            public void Configure(string name, JwtBearerOptions options)
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidIssuer = _azureOptions.Issuer,
                    ValidAudience = _azureOptions.Audience
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = AuthenticationFailedAsync
                };
                options.Authority = $"https://login.microsoftonline.com/{_azureOptions.Tenant}/v2.0/";
                options.Audience = _azureOptions.Audience;
                options.RequireHttpsMetadata = false;
            }
            private Task AuthenticationFailedAsync(AuthenticationFailedContext arg)
            {
                var s = $"AuthenticationFailed: {arg.Exception.Message}";
                arg.Response.ContentLength = s.Length;
                arg.Response.Body.Write(Encoding.UTF8.GetBytes(s), 0, s.Length);
                return Task.FromResult(0);
            }

            public void Configure(JwtBearerOptions options)
            {
                Configure(Options.DefaultName, options);
            }
        }
    }
}
