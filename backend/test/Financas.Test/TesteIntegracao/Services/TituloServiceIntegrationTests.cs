using Financas.Api.AutoMapper;
using Financas.Api.Contract.Titulos;
using Financas.Api.Domain.Enums;
using Financas.Api.Domain.Models;
using Financas.Api.Domain.Repository.Classes;
using Financas.Api.Domain.Services.Classes;
using Financas.Test.Fixtures;
using Financas.Test.UnitTests.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Financas.Test.IntegrationTests.Services
{
    [Trait("Category", "Services")]
    public class TituloServiceIntegrationTests : IntegrationTestBase
    {
        private readonly TituloService _service;
        protected readonly NaturezaDeLancamento NaturezaDeLancamentoPadrao;

        public TituloServiceIntegrationTests() : base()
        {
            var mapper = new AutoMapper.MapperConfiguration(x => x.AddProfile<TituloProfile>()).CreateMapper();

            _service = new TituloService(
                new TituloRepository(Context),
                new NaturezaDeLancamentoRepository(Context),
                mapper);

            NaturezaDeLancamentoPadrao = NaturezaDeLancamentoFixture.CriarNaturezaDeLancamentoValido(0);
        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            NaturezaDeLancamentoPadrao.IdUsuario = UsuarioPadrao.Id;

            await Context.NaturezaDeLancamento.AddAsync(NaturezaDeLancamentoPadrao);
            await Context.SaveChangesAsync();
        }

        [Fact]
        public async Task Adicionar_DevePersistirNoBanco()
        {
            // Arrange
            var request = new TituloRequestContract
            {
                TipoTitulo = TipoTituloEnum.Gasto,
                Descricao = "Teste",
                Observacao = "Observação",
                ValorOriginal = 100,
                IdNaturezaDeLancamento = NaturezaDeLancamentoPadrao.Id,
            };

            // Act
            var resultado = await _service.Adicionar(request, UsuarioPadrao.Id);

            // Assert
            var entidade = await Context.Titulo.FirstOrDefaultAsync(x => x.Id == resultado.Id);
            Assert.NotNull(entidade);
            Assert.Equal(request.Descricao, entidade.Descricao);
            Assert.Equal(request.Observacao, entidade.Observacao);
            Assert.Equal(request.ValorOriginal, entidade.ValorOriginal);
            Assert.Equal(request.TipoTitulo, entidade.TipoTitulo);
            Assert.True(entidade.Id > 0);
            Assert.True(entidade.DataCadastro.Date == DateTime.Today);
        }

        [Fact]
        public async Task Atualizar_DeveAtualizarRegistroNoBanco()
        {
            // Arrange
            var natureza = new NaturezaDeLancamento { Descricao = "Natureza Teste", IdUsuario = UsuarioPadrao.Id };
            await Context.NaturezaDeLancamento.AddAsync(natureza);
            await Context.SaveChangesAsync();

            var entidade = new Titulo
            {
                TipoTitulo = TipoTituloEnum.Gasto,
                Descricao = "Teste",
                Observacao = "Observação",
                ValorOriginal = 100,
                IdUsuario = UsuarioPadrao.Id,
                IdNaturezaDeLancamento = NaturezaDeLancamentoPadrao.Id
            };

            await Context.Titulo.AddAsync(entidade);
            await Context.SaveChangesAsync();

            var request = new TituloRequestContract
            {
                Descricao = "Atualizado",
                Observacao = "Nova Observação",
                ValorOriginal = 200,
                IdNaturezaDeLancamento = NaturezaDeLancamentoPadrao.Id
            };

            // Act
            var resultado = await _service.Atualizar(entidade.Id, request, UsuarioPadrao.Id);

            // Assert
            var entidadeAtualizada = await Context.Titulo.FirstOrDefaultAsync(x => x.Id == entidade.Id);
            Assert.Equal(request.Descricao, entidadeAtualizada!.Descricao);
            Assert.Equal(request.Observacao, entidadeAtualizada!.Observacao);
            Assert.Equal(request.ValorOriginal, entidadeAtualizada!.ValorOriginal);
        }

        [Fact]
        public async Task Inativar_DeveRemoverDoBanco()
        {
            // Arrange
            var entidade = new Titulo
            {
                TipoTitulo = TipoTituloEnum.Recebimento,
                Descricao = "Teste",
                Observacao = "Observação",
                ValorOriginal = 100,
                IdUsuario = UsuarioPadrao.Id,
                IdNaturezaDeLancamento = NaturezaDeLancamentoPadrao.Id
            };

            await Context.Titulo.AddAsync(entidade);
            await Context.SaveChangesAsync();

            // Act
            await _service.Inativar(entidade.Id, UsuarioPadrao.Id);

            // Assert
            var entidadeAtualizada = await Context.Titulo.FirstOrDefaultAsync(x => x.Id == entidade.Id);
            Assert.True(entidadeAtualizada!.DataInativacao is not null);
        }
    }
}
