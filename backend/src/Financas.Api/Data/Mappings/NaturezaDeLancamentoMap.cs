using Financas.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Financas.Api.Data.Mappings
{
    public class NaturezaDeLancamentoMap : IEntityTypeConfiguration<NaturezaDeLancamento>
    {
         public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<NaturezaDeLancamento> builder)
        {
            builder.ToTable("naturezadelancamento")
            .HasKey(p => p.Id);

            builder.HasOne(p => p.Usuario)
            .WithMany()
            .HasForeignKey(fk => fk.IdUsuario);

            builder.Property(p => p.Descricao)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(p => p.Observacao)
            .HasColumnType("VARCHAR");

            builder.Property(p => p.DataCadastro)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.Property(p => p.DataInativacao)
            .HasColumnType("timestamp");
        }
    }
}