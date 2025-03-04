using Financas.Api.AutoMapper;
using Financas.Api.Contract.NaturezaDeLancamento;
using Financas.Api.Domain.Repository.Classes;
using Financas.Api.Domain.Services.Classes;
using Financas.Test.Fixtures;
using Financas.Test.UnitTests.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Financas.Test.IntegrationTests.Services
{
    [Trait("Category", "Services")]
    public class NaturezaDeLancamentoServiceIntegrationTests : IntegrationTestBase
    {
        private readonly NaturezaDeLancamentoService _service;

        public NaturezaDeLancamentoServiceIntegrationTests() : base()
        {
            var mapper = new AutoMapper.MapperConfiguration(x => x.AddProfile<NaturezaDeLancamentoProfile>()).CreateMapper();

            _service = new NaturezaDeLancamentoService(new NaturezaDeLancamentoRepository(Context), mapper);
        }

        [Fact]
        public async Task Adicionar_DevePersistirNoBanco()
        {
            // Arrange
            var request = new NaturezaDeLancamentoRequestContract 
            { 
                Descricao = "Teste", 
                Observacao = "Observação" 
            };

            // Act
            var resultado = await _service.Adicionar(request, UsuarioPadrao.Id);

            // Assert
            var entidade = await Context.NaturezaDeLancamento.FirstOrDefaultAsync(x => x.Id == resultado.Id);
            Assert.NotNull(entidade);
            Assert.Equal(request.Descricao, entidade.Descricao);
            Assert.Equal(request.Observacao, entidade.Observacao);
            Assert.True(entidade.Id > 0);
            Assert.True(entidade.DataInativacao is null);
            Assert.True(entidade.DataCadastro.Date == DateTime.Today);
        }

        [Fact]
        public async Task Atualizar_DeveAtualizarRegistroNoBanco()
        {
            // Arrange
            var entidade = NaturezaDeLancamentoFixture.CriarNaturezaDeLancamentoValido(UsuarioPadrao.Id);

            await Context.NaturezaDeLancamento.AddAsync(entidade);
            await Context.SaveChangesAsync();

            var request = new NaturezaDeLancamentoRequestContract 
            { 
                Descricao = "Atualizado", 
                Observacao = "Nova Observação" 
            };

            // Act
            var resultado = await _service.Atualizar(entidade.Id, request, UsuarioPadrao.Id);

            // Assert
            var entidadeAtualizada = await Context.NaturezaDeLancamento.FirstOrDefaultAsync(x => x.Id == entidade.Id);
            Assert.Equal(request.Descricao, entidadeAtualizada!.Descricao);
            Assert.Equal(request.Observacao, entidadeAtualizada!.Observacao);
        }

       
        [Fact]
        public async Task Inativar_DeveInativarNoBanco()
        {
            // Arrange
            var entidade = NaturezaDeLancamentoFixture.CriarNaturezaDeLancamentoValido(UsuarioPadrao.Id);

            await Context.NaturezaDeLancamento.AddAsync(entidade);
            await Context.SaveChangesAsync();

            // Act
            await _service.Inativar(entidade.Id, UsuarioPadrao.Id);

            // Assert
            var entidadeAtualizada = await Context.NaturezaDeLancamento.FirstOrDefaultAsync(x => x.Id == entidade.Id);
            Assert.NotNull(entidadeAtualizada!.DataInativacao);
        }
    }
}
