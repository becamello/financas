using Financas.Api.Contract.Usuario;
using System.Net.Http.Json;
using System.Net;
using Financas.Test.IntegrationTests.Abstractions;
using Financas.Api.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using Financas.Test.Fixtures;

namespace Financas.Test.IntegrationTests.Controllers
{
    [Trait("Category", "API")]
    public class UsuarioControllerTests : IClassFixture<ApiIntegrationTestBase>, IAsyncLifetime
    {
        private readonly HttpClient _client;
        private readonly ApplicationContext _context;
        private readonly ApiIntegrationTestBase _factory;

        public UsuarioControllerTests(ApiIntegrationTestBase factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        }

        public async Task InitializeAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CriarUsuarioPadraoAsync();

            var usuarioLogado = await _factory.ObterTokenAsync(_client);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuarioLogado.Token);
        }

        public async Task DisposeAsync() => await _context.Database.EnsureDeletedAsync();

        private async Task CriarUsuarioPadraoAsync()
        {
            var usuario = UsuarioFixture.CriarUsuarioValidoComHash();

            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Autenticar_DeveRetornarOk_QuandoCredenciaisValidas()
        {
            // Arrange
            var request = UsuarioFixture.CriarUsuarioValido();

            // Act
            var response = await _client.PostAsJsonAsync("/usuarios/login", request);

            // Assert
            var result = await response.Content.ReadFromJsonAsync<UsuarioLoginResponseContract>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(request.Email, result!.Email);
            Assert.True(result.Id > 0);
            Assert.True(!string.IsNullOrEmpty(result.Token));
        }

        [Fact]
        public async Task Autenticar_DeveRetornarUnauthorized_QuandoCredenciaisInvalidas()
        {
            // Arrange
            var request = new UsuarioLoginRequestContract 
            { 
                Email = "invalido@teste.com", 
                Senha = "senhaErrada" 
            };

            // Act
            var response = await _client.PostAsJsonAsync("/usuarios/login", request);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarCreated_QuandoUsuarioValido()
        {
            // Arrange
            var request = new UsuarioRequestContract 
            { 
                Email = "novo@teste.com", 
                Senha = "SenhaForte123" 
            };

            // Act
            var response = await _client.PostAsJsonAsync("/usuarios", request);

            // Assert
            var result = await response.Content.ReadFromJsonAsync<UsuarioResponseContract>();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(request.Email, result!.Email);
        }

        [Fact]
        public async Task Obter_DeveRetornarOk_QuandoUsuariosExistem()
        {
            // Arrange && Act
            var response = await _client.GetAsync("/usuarios");

            // Assert
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<UsuarioResponseContract>>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(result!.ToList().Count > 0);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNotFound_QuandoUsuarioNaoExiste()
        {
            // Arrange
            var usuarioLogado = await _factory.ObterTokenAsync(_client);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuarioLogado.Token);

            // Act
            var response = await _client.GetAsync("/usuarios/9999");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarOk_QuandoUsuarioAtualizado()
        {
            // Arrange
            var request = new UsuarioRequestContract 
            { 
                Email = "atualizado@teste.com", 
                Senha = "SenhaNova123" 
            };

            // Act
            var response = await _client.PutAsJsonAsync("/usuarios/1", request);

            // Assert
            var result = await response.Content.ReadFromJsonAsync<UsuarioResponseContract>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(request.Email, result!.Email);
        }

        [Fact]
        public async Task Deletar_DeveRetornarNoContent_QuandoUsuarioExcluido()
        {
            // Arrange & Act
            var response = await _client.DeleteAsync("/usuarios/1");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
