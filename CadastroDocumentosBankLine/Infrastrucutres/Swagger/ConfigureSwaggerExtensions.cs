using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CadastroDocumentosBankLine.WebAPI.Infrastrucutres.Swagger
{
    public static class ConfigureSwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            var apiVersionDescriptionProvider = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    var openApiInfo = new OpenApiInfo()
                    {
                        Version = description.ApiVersion.ToString(),
                        Title = "Cadastro de Documentos - API",
                        Description = "Api de cadastro de documentos"
                    };

                    if (!options.SwaggerGeneratorOptions.SwaggerDocs.ContainsKey($"CadastroDocumentosBankLine.WebAPI.Specification{description.GroupName}"))
                    {
                        options.SwaggerDoc(
                            $"CadastroDocumentosBankLine.WebAPI.Specification{description.GroupName}",
                            openApiInfo);
                    }
                }

                options.DocInclusionPredicate((documentName, apiDescription) =>
                {
                    var actionApiVersionModel = apiDescription.ActionDescriptor
                    .GetApiVersionModel();

                    if (actionApiVersionModel == null)
                        return true;


                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                    {
                        return actionApiVersionModel.DeclaredApiVersions.Any(v =>
                        $"CadastroDocumentosBankLine.WebAPI.Specificationv{v.ToString()}" == documentName);
                    }
                    return actionApiVersionModel.ImplementedApiVersions.Any(v =>
                        $"CadastroDocumentosBankLine.WebAPI.Specificationv{v.ToString()}" == documentName);
                });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
