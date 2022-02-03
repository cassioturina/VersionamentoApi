using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;
using System.Reflection;
using VersionamentoApi.Filters;

namespace VersionamentoApi.Configurations
{
    public static class SwaggerServcieCollection
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiVersioning(
             options =>
             {
                 options.AssumeDefaultVersionWhenUnspecified = true;
                 options.ReportApiVersions = true;
                 options.DefaultApiVersion = new ApiVersion(1, 0);
             });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(
                options =>

                {
                    //options.IgnoreObsoleteActions();


                    options.OperationFilter<SwaggerDefaultValues>();
                    options.AddSecurityDefinition("apiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Name = "x-api-key",
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                        Scheme = "apiKey"
                    });
                    options.OperationFilter<AuthenticationRequirementsOperationFilter>();

                    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                    var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                    var path = Path.Combine(basePath, fileName);
                    options.IncludeXmlComments(path);

                });
        }
    }
}
