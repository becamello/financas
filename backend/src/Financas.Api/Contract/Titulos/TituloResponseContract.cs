
using Financas.Api.Domain.Enums;

namespace Financas.Api.Contract.Titulos
{
    public class TituloResponseContract : TituloRequestContract
    {
        public long Id { get; set; }
        public long IdUsuario { get; set; }
        public long IdNaturezaDeLancamento { get; set; }
        public TipoTituloEnum TipoTitulo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataInativacao { get; set; }
    }
}