﻿using CadastroDocumentosBankLine.ApplicationService;
using CadastroDocumentosBankLine.Domain.IServices;
using CadastroDocumentosBankLine.Infra.Bus.Producers;
using Microsoft.Extensions.DependencyInjection;

namespace CadastroDocumentosBankLine.Infra.CrossCutting
{
    public static class DependenciesRegistration
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            //Application service
            services.AddTransient<ICadastroDocumentosService, CadastroDocumentosService>();
            services.AddTransient<IProcessoProducer, ProcessoProducer>();
            services.AddTransient<IDocumentosFilaEnviadorService, DocumentosFilaEnviadorService>();
        }
    }
}
