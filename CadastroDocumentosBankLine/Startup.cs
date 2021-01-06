using Autofac;
using CadastroDocumentosBankLine.Infra.Bus.Configurations;
using CadastroDocumentosBankLine.Infra.Bus.Messages;
using CadastroDocumentosBankLine.Infra.CrossCutting;
using CadastroDocumentosBankLine.WebAPI.Infrastrucutres.Swagger;
using CadastroDocumentosBankLine.WebAPI.Infrastrucutres.Versioning;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CadastroDocumentosBankLine
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IBusControl>();
            services.AddSingleton<IPublishEndpoint>();
            services.AddSingleton<IBus>();

            services.RegisterDependencies();

            services.AddMvc(m => m.EnableEndpointRouting = false);
            services.AddVersioning();

            services.AddMvc();

            services.AddSwagger();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var rabbitMqSettingsNet = new RabbitMqSettings(Configuration);

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(rabbitMqSettingsNet.RabbitMqUri
                //    , host =>
                //{
                //    host.Username(rabbitMqSettingsNet.UserName);
                //    host.Password(rabbitMqSettingsNet.Password);
                //}
                );

                cfg.Publish<ObtidoProcesso>(x => x.BindQueue($"{typeof(ObtidoProcesso).Namespace}:{nameof(ObtidoProcesso)}", rabbitMqSettingsNet.EnviarDocumentosServiceQueue));

            });

            var builder = new ContainerBuilder();

            builder.Register(c => bus)
                 .As<IBusControl>()
                 .As<IPublishEndpoint>()
                 .As<IBus>()
                 .SingleInstance();

            var container = builder.Build();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(cfg =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    cfg.SwaggerEndpoint($"/swagger/CadastroDocumentosBankLine.WebAPI.Specification{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                    cfg.DocumentTitle = "Cadastro de Documentos - API";
                }
            });
        }
    }
}
