using Financas.Api.Data.Mappings;
using Financas.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Financas.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }

         public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}

           protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }
    }
}