using Financas.Api.Contract.NaturezaDeLancamento;
using System.Net.Http.Json;
using System.Net;
using Financas.Test.IntegrationTests.Abstractions;
using Financas.Api.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using Financas.Test.Fixtures;
using Financas.Api.Contract.Usuario;
using Microsoft.EntityFrameworkCore;

namespace Financas.Test.IntegrationTests.Controllers
{
    [Trait("Category", "API")]
    public class NaturezaDeLancamentoControllerTests : IClassFixture<ApiIntegrationTestBase>, IAsyncLifetime
    {
        private readonly HttpClient _client;
        private readonly ApplicationContext _context;
        private readonly ApiIntegrationTestBase _factory;

        private long _usuarioId;
        private long _naturezaDeLancamentoId;

        public NaturezaDeLancamentoControllerTests(ApiIntegrationTestBase factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

            var scope = factory.Services.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        }

        public async Task InitializeAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            _usuarioId = await CriarUsuarioAsync();
            await RealizarLoginAsync();
            _naturezaDeLancamentoId = await CriarNaturezaDeLancamentoAsync(_usuarioId);
        }

        public async Task DisposeAsync() => await _context.Database.EnsureDeletedAsync();

        private async Task<long> CriarUsuarioAsync()
        {
            var usuario = UsuarioFixture.CriarUsuarioValidoComHash();
            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario.Id; 
        }

        private async Task RealizarLoginAsync()
        {
            var loginRequest = UsuarioFixture.CriarUsuarioValido();
            var loginResponse = await _client.PostAsJsonAsync("/usuarios/login", loginRequest);
            loginResponse.EnsureSuccessStatusCode();
            var loginResult = await loginResponse.Content.ReadFromJsonAsync<UsuarioLoginResponseContract>();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult!.Token);
        }
        private async Task<long> CriarNaturezaDeLancamentoAsync(long usuarioId)
        {
            var naturezaDeLancamento = NaturezaDeLancamentoFixture.CriarNaturezaDeLancamentoValido(usuarioId);
            await _context.NaturezaDeLancamento.AddAsync(naturezaDeLancamento);
            await _context.SaveChangesAsync();
            return naturezaDeLancamento.Id; 
        }

        [Fact]
        public async Task Adicionar_DeveRetornarCreated_QuandoNaturezaDeLancamentoValida()
        {
            // Arrange
            var request = new NaturezaDeLancamentoRequestContract
            {
                Descricao = "Natureza de Lançamento 1",
                Observacao = "Descrição da natureza"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/naturezasdelancamento", request);

            // Assert
            var result = await response.Content.ReadFromJsonAsync<NaturezaDeLancamentoResponseContract>();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(request.Descricao, result!.Descricao);
        }

        [Fact]
        public async Task Obter_DeveRetornarOk_QuandoNaturezasDeLancamentoExistem()
        {
            // Arrange && Act
            var response = await _client.GetAsync("/naturezasdelancamento");

            // Assert
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<NaturezaDeLancamentoResponseContract>>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNotFound_QuandoNaturezaDeLancamentoNaoExiste()
        {
            // Arrange
            var idNaoExistente = 9999;

            // Act
            var response = await _client.GetAsync($"/naturezasdelancamento/{idNaoExistente}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarOk_QuandoNaturezaDeLancamentoAtualizada()
        {
            // Arrange
            var request = new NaturezaDeLancamentoRequestContract
            {
                Descricao = "Natureza Atualizada",
                Observacao = "Descrição da natureza atualizada"
            };

            var naturezaDeLancamento = await _context.NaturezaDeLancamento.FirstOrDefaultAsync();

            // Act
            var response = await _client.PutAsJsonAsync($"/naturezasdelancamento/{naturezaDeLancamento.Id}", request);

            // Assert
            var result = await response.Content.ReadFromJsonAsync<NaturezaDeLancamentoResponseContract>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(request.Descricao, result!.Descricao);
            Assert.Equal(request.Observacao, result!.Observacao);
        }

        [Fact]
        public async Task Deletar_DeveRetornarNoContent_QuandoNaturezaDeLancamentoExcluida()
        {
            // Arrange & Act
            var naturezaDeLancamento = await _context.NaturezaDeLancamento.FirstOrDefaultAsync();

            var response = await _client.DeleteAsync($"/naturezasdelancamento/{naturezaDeLancamento.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
