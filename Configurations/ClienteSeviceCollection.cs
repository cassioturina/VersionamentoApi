using ACCVeiculos.Core.Configuracoes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace VersionamentoApi.Configurations
{
    public static class ClienteSeviceCollection
    {
        public static void AddClienteConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            var cliente = new ClienteConfiguration
            {
                Clientes = configuration.GetSection("Clientes")?.Get<List<ClienteApi>>()
            };
            services.AddSingleton(typeof(IClienteConfiguration), cliente);
        }
    }
}
