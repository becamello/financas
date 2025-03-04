using Financas.Api.Domain.Models;
using Financas.Test.UnitTests.Abstractions;

namespace Financas.Test.UnitTests.Domain
{
    [Trait("Category", "Domain")]
    public class UsuarioTests : UnitTestBase
    {
        [Fact]
        public void Usuario_DeveSerValido_QuandoTodosOsCamposObrigatoriosEstiveremPreenchidos()
        {
            // Arrange
            var model = new Usuario
            {
                Email = "usuario@email.com",
                Senha = "SenhaForte123"
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Empty(validationResults);
        }

        [Fact]
        public void Usuario_DeveSerInvalido_QuandoEmailNaoForInformado()
        {
            // Arrange
            var model = new Usuario
            {
                Senha = "SenhaForte123"
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Email"));
        }

        [Fact]
        public void Usuario_DeveSerInvalido_QuandoSenhaNaoForInformada()
        {
            // Arrange
            var model = new Usuario
            {
                Email = "usuario@email.com"
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Senha"));
        }
    }
}
