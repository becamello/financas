
using System.ComponentModel.DataAnnotations;
using Financas.Api.Domain.Enums;

namespace Financas.Api.Contract.Titulos
{
    public class TituloRequestContract
    {
        [Required]
        public TipoTituloEnum TipoTitulo { get; set; }
        [Required]
        public long IdNaturezaDeLancamento { get; set; }
        [Required]
        public string Descricao { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;
        public double ValorOriginal { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
}