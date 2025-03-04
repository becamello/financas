using System.ComponentModel.DataAnnotations;
using Financas.Api.Domain.Enums;

namespace Financas.Api.Domain.Models
{
    public class Titulo
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public TipoTituloEnum? TipoTitulo { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "O ID do usuário deve ser maior que zero.")]
        public long IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "O ID da natureza de lançamento deve ser maior que zero.")]
        public long IdNaturezaDeLancamento { get; set; }
        public NaturezaDeLancamento NaturezaDeLancamento { get; set; }

        [Required(ErrorMessage = "O campo de descrição é obrigatório.")]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor original deve ser maior que zero.")]
        public double ValorOriginal { get; set; }

        public string Observacao { get; set; } = string.Empty;

        [Required]
        public DateTime DataCadastro { get; set; }

        public DateTime? DataPagamento { get; set; }
        public DateTime? DataInativacao { get; set; }
    }
}