using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace CadastroDocumentosBankLine.WebAPI.Infrastrucutres.Versioning
{
    public static class ConfigureVersioningExtensions
    {
        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(cfg =>
            {
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.DefaultApiVersion = new ApiVersion(1, 0);
                cfg.ReportApiVersions = true;
                cfg.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddVersionedApiExplorer(cfg =>
            {
                cfg.GroupNameFormat = "'v'VV";
            });
        }
    }
}
