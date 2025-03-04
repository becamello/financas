using Financas.Api.Domain.Enums;
using Financas.Api.Domain.Models;
using Financas.Test.UnitTests.Abstractions;

namespace Financas.Test.UnitTests.Domain
{
    [Trait("Category", "Domain")]
    public class TituloTests : UnitTestBase
    {
        [Fact]
        public void Titulo_DeveSerValido_QuandoTodosOsCamposObrigatoriosEstiveremPreenchidos()
        {
            // Arrange
            var model = new Titulo
            {
                TipoTitulo = TipoTituloEnum.Recebimento,
                IdUsuario = 1,
                IdNaturezaDeLancamento = 1,
                Descricao = "Pagamento de serviço",
                ValorOriginal = 100.50,
                DataCadastro = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Empty(validationResults);
        }

        [Fact]
        public void Titulo_DeveSerInvalido_QuandoTipoTituloNaoForInformado()
        {
            // Arrange
            var model = new Titulo
            {
                IdUsuario = 1,
                IdNaturezaDeLancamento = 1,
                Descricao = "Pagamento de serviço",
                ValorOriginal = 100.50,
                DataCadastro = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("TipoTitulo"));
        }

        [Fact]
        public void Titulo_DeveSerInvalido_QuandoIdUsuarioNaoForInformado()
        {
            // Arrange
            var model = new Titulo
            {
                TipoTitulo = TipoTituloEnum.Recebimento,
                IdNaturezaDeLancamento = 1,
                Descricao = "Pagamento de serviço",
                ValorOriginal = 100.50,
                DataCadastro = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("IdUsuario"));
        }

        [Fact]
        public void Titulo_DeveSerInvalido_QuandoIdNaturezaDeLancamentoNaoForInformado()
        {
            // Arrange
            var model = new Titulo
            {
                TipoTitulo = TipoTituloEnum.Recebimento,
                IdUsuario = 1,
                Descricao = "Pagamento de serviço",
                ValorOriginal = 100.50
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("IdNaturezaDeLancamento"));
        }

        [Fact]
        public void Titulo_DeveSerInvalido_QuandoDescricaoNaoForInformada()
        {
            // Arrange
            var model = new Titulo
            {
                TipoTitulo = TipoTituloEnum.Gasto,
                IdUsuario = 1,
                IdNaturezaDeLancamento = 1,
                ValorOriginal = 100.50
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Descricao"));
        }

        [Fact]
        public void Titulo_DeveSerInvalido_QuandoValorOriginalNaoForInformado()
        {
            // Arrange
            var model = new Titulo
            {
                TipoTitulo = TipoTituloEnum.Gasto,
                IdUsuario = 1,
                IdNaturezaDeLancamento = 1,
                Descricao = "Pagamento de serviço"
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("ValorOriginal"));
        }
    }
}
