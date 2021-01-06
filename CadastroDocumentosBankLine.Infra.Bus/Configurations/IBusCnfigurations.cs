using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroDocumentosBankLine.Infra.Bus.Configurations
{
    public interface IBusConfiguration
    {
        IBusControl CreateBus();
    }
}
