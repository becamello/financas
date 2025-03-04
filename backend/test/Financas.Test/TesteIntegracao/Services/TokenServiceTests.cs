using Financas.Api.Domain.Services.Classes;
using Financas.Test.Fixtures;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;

namespace Financas.Test.IntegrationTests.Services
{
    [Trait("Category", "Services")]
    public class TokenServiceTests
    {
        [Fact]
        public void GerarToken_DeveRetornarToken_QuandoUsuarioValido()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(cfg => cfg["KeySecret"]).Returns("supersecreta-chave-para-teste");
            mockConfig.Setup(cfg => cfg["HorasValidadeToken"]).Returns("1");

            var tokenService = new TokenService(mockConfig.Object);
            var usuario = UsuarioFixture.CriarUsuarioValido();

            string token = tokenService.GerarToken(usuario);

            Assert.False(string.IsNullOrEmpty(token));
        }

        [Fact]
        public void GerarToken_DeveConterClaims_CorretosNoToken()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(cfg => cfg["KeySecret"]).Returns("supersecreta-chave-para-teste");
            mockConfig.Setup(cfg => cfg["HorasValidadeToken"]).Returns("1");

            var tokenService = new TokenService(mockConfig.Object);
            var usuario = UsuarioFixture.CriarUsuarioValido();

            string token = tokenService.GerarToken(usuario);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var nameIdentifierClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid");
            var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "email");

            Assert.Equal(usuario.Id.ToString(), nameIdentifierClaim?.Value);
            Assert.Equal(usuario.Email, emailClaim?.Value);
        }
    }
}
