using Financas.Api.Contract.Usuario;
using Financas.Api.Data;
using Financas.Test.Fixtures;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace Financas.Test.IntegrationTests.Abstractions
{
    public class ApiIntegrationTestBase : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                var connectionString = ConnectionStringFactory.Create();

                services.AddDbContext<ApplicationContext>(options =>
                    options.UseNpgsql(connectionString));
            });
        }

        public async Task<UsuarioLoginResponseContract> ObterTokenAsync(HttpClient client)
        {
            var response = await client.PostAsJsonAsync("/usuarios/login", UsuarioFixture.CriarUsuarioValido());

            var result = await response.Content.ReadFromJsonAsync<UsuarioLoginResponseContract>();

            return result!;
        }
    }
}
