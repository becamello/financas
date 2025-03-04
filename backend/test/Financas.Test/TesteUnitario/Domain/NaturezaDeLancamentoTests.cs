using Financas.Api.Domain.Models;
using Financas.Test.UnitTests.Abstractions;

namespace Financas.Test.UnitTests.Domain
{
    [Trait("Category", "Domain")]
    public class NaturezaDeLancamentoTests : UnitTestBase
    {
        [Fact]
        public void NaturezaDeLancamento_DeveSerValida_QuandoTodosOsCamposObrigatoriosEstiveremPreenchidos()
        {
            // Arrange
            var model = new NaturezaDeLancamento
            {
                IdUsuario = 1,
                Descricao = "Teste de Lançamento"
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Empty(validationResults);
        }

        [Fact]
        public void NaturezaDeLancamento_DeveSerInvalida_QuandoIdUsuarioForZero()
        {
            // Arrange
            var model = new NaturezaDeLancamento
            {
                Descricao = "Teste de Lançamento"
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("IdUsuario"));
        }

        [Fact]
        public void NaturezaDeLancamento_DeveSerInvalida_QuandoDescricaoNaoForInformada()
        {
            // Arrange
            var model = new NaturezaDeLancamento
            {
                IdUsuario = 1
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Descricao"));
        }
    }
}
