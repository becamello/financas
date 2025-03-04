using Financas.Api.AutoMapper;
using Financas.Api.Data;
using Financas.Api.Domain.Models;
using Financas.Test.Fixtures;
using Financas.Test.IntegrationTests.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Financas.Test.UnitTests.Abstractions
{
    public abstract class IntegrationTestBase : IAsyncLifetime
    {
        protected readonly ApplicationContext Context;
        protected readonly Usuario UsuarioPadrao;

        protected IntegrationTestBase()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseNpgsql(ConnectionStringFactory.Create())
                .Options;

            var mapper = new AutoMapper.MapperConfiguration(x => x.AddProfile<NaturezaDeLancamentoProfile>()).CreateMapper();

            Context = new ApplicationContext(options);
            UsuarioPadrao = UsuarioFixture.CriarUsuarioValido();
        }

        public virtual async Task InitializeAsync()
        {
            await Context.Database.EnsureCreatedAsync();
            await Context.Usuario.AddAsync(UsuarioPadrao);
            await Context.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            await Context.Database.EnsureDeletedAsync();
        }
    }
}
