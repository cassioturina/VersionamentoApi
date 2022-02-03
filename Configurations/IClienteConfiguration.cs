using System.Collections.Generic;

namespace ACCVeiculos.Core.Configuracoes
{
    public interface IClienteConfiguration
    {
        public IList<ClienteApi> Clientes { get; set; }
    }

    public class ClienteConfiguration : IClienteConfiguration
    {
        public IList<ClienteApi> Clientes { get; set; }
    }
    public class ClienteApi
    {
        public string Nome { get; set; }
        public string Key { get; set; }
    }
}
