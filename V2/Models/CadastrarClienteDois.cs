using System.ComponentModel.DataAnnotations;

namespace VersionamentoApi.V2
{
    public class CadastrarClienteDois
    {
        [StringLength(10, ErrorMessage = "Máximo {1} caracteres")]
        public string Nome { get; set; }

        [StringLength(10, ErrorMessage = "Máximo {1} caracteres")]
        public string SobreNome { get; set; }
    }
}
