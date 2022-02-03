using System.ComponentModel.DataAnnotations;

namespace VersionamentoApi.V1
{
    public class CadastrarClienteDois
    {
        [StringLength(10, ErrorMessage = "Máximo {1} caracteres")]
        public string Nome { get; set; }
    }
}
