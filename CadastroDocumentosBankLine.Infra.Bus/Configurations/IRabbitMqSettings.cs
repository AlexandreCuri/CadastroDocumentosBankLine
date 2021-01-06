using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroDocumentosBankLine.Infra.Bus.Configurations
{
    public interface IRabbitMqSettings
    {
        string RabbitMqUri { get; }
        string UserName { get; }
        string Password { get; }
        string EnviarDocumentosServiceQueue { get; }
    }
}
