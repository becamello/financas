using Financas.Api.Domain.Models;

namespace Financas.Test.Fixtures
{
    public static class NaturezaDeLancamentoFixture
    {
        public static NaturezaDeLancamento CriarNaturezaDeLancamentoValido(long idUsuario) => new()
        {
            Descricao = "Teste",
            Observacao = "Observação",
            IdUsuario = idUsuario
        };
    }
}
