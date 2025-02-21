using Financas.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Financas.Api.Data.Mappings
{
    public class TituloMap : IEntityTypeConfiguration<Titulo>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Titulo> builder)
        {
            builder.ToTable("titulo")
            .HasKey(p => p.Id);

            builder.HasOne(p => p.Usuario)
            .WithMany()
            .HasForeignKey(fk => fk.IdUsuario);

            builder.HasOne(p => p.NaturezaDeLancamento)
            .WithMany()
            .HasForeignKey(fk => fk.IdNaturezaDeLancamento);

            builder.Property(p => p.Descricao)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(p => p.ValorOriginal)
            .HasColumnType("double precision")
            .IsRequired();

            builder.Property(p => p.Observacao)
            .HasColumnType("VARCHAR");

            builder.Property(p => p.DataCadastro)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.Property(p => p.DataPagamento)
            .HasColumnType("timestamp")
            .IsRequired();
            
            builder.Property(p => p.DataInativacao)
            .HasColumnType("timestamp");
        }
    }
}