using Financas.Api.Domain.Enums;
using Financas.Api.Domain.Models;

namespace Financas.Test.Fixtures
{
    public static class TituloFixture
    {
        public static Titulo CriarTituloValido(long idUsuario, long idNaturezaDeLancamento) => new()
        {
            TipoTitulo = TipoTituloEnum.Gasto, 
            IdUsuario = idUsuario,
            IdNaturezaDeLancamento = idNaturezaDeLancamento,
            Descricao = "Titulo de Teste",
            ValorOriginal = 100.0,
            Observacao = "Observação do Título",
            DataCadastro = DateTime.Now
        };
    }
}
