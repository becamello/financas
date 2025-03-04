using Financas.Api.Contract.Titulos;
using Financas.Api.Data;
using Financas.Test.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using Financas.Test.IntegrationTests.Abstractions;
using Financas.Api.Contract.Usuario;
using Microsoft.EntityFrameworkCore;
using Financas.Api.Domain.Enums;
using Financas.Api.Domain;
using Financas.Api.Domain.Models;

namespace Financas.Test.IntegrationTests.Controllers
{
    [Trait("Category", "API")]
    public class TituloControllerTests : IClassFixture<ApiIntegrationTestBase>, IAsyncLifetime
    {
        private readonly HttpClient _client;
        private readonly ApplicationContext _context;
        private readonly ApiIntegrationTestBase _factory;

        private long _usuarioId;
        private long _naturezaDeLancamentoId;
        
        public TituloControllerTests(ApiIntegrationTestBase factory)
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
            await CriarTituloPadraoAsync(_usuarioId, _naturezaDeLancamentoId);
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

        private async Task<Titulo> CriarTituloPadraoAsync(long usuarioId, long naturezaDeLancamentoId)
        {
            var titulo = TituloFixture.CriarTituloValido(usuarioId, naturezaDeLancamentoId);
            await _context.Titulo.AddAsync(titulo);
            await _context.SaveChangesAsync();
            return titulo;
        }

        [Fact]
        public async Task Adicionar_DeveRetornarCreated_QuandoTituloValido()
        {
            // Arrange
            var request = new TituloRequestContract
            {
                TipoTitulo = TipoTituloEnum.Gasto,
                IdNaturezaDeLancamento = _naturezaDeLancamentoId,
                Descricao = "Novo Título",
                ValorOriginal = 150.00,
                Observacao = "Observação do Título",
            };

            // Act
            var response = await _client.PostAsJsonAsync("/titulos", request);

            // Assert
            var result = await response.Content.ReadFromJsonAsync<TituloResponseContract>();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(request.Descricao, result!.Descricao);
        }

        [Fact]
        public async Task Obter_DeveRetornarOk_QuandoTitulosExistem()
        {
            // Arrange && Act
            var response = await _client.GetAsync("/titulos");

            // Assert
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<TituloResponseContract>>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNotFound_QuandoTituloNaoExiste()
        {
            // Arrange
            var idNaoExistente = 9999;

            // Act
            var response = await _client.GetAsync($"/titulos/{idNaoExistente}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarOk_QuandoTituloAtualizado()
        {
            // Arrange
            var tituloExistente = await _context.Titulo.FirstOrDefaultAsync();

            var request = new TituloRequestContract
            {
                TipoTitulo = TipoTituloEnum.Gasto,
                IdNaturezaDeLancamento = _naturezaDeLancamentoId,
                Descricao = "Titulo Atualizado",
                ValorOriginal = 200.00,
                Observacao = "Observação atualizada",
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/titulos/{tituloExistente.Id}", request);

            // Assert
            var result = await response.Content.ReadFromJsonAsync<TituloResponseContract>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(request.Descricao, result!.Descricao);
            Assert.Equal(request.Observacao, result!.Observacao);
        }

        [Fact]
        public async Task Deletar_DeveRetornarNoContent_QuandoTituloExcluido()
        {
            // Arrange
            var tituloExistente = await _context.Titulo.FirstOrDefaultAsync();

            // Act
            var response = await _client.DeleteAsync($"/titulos/{tituloExistente.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
