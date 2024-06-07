using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ms_spa.Api.Domain.Models;

namespace ms_spa.Api.Data.Mapping
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        private const string varchar = "VARCHAR";

        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("cliente")
              .HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasColumnType(varchar)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType(varchar)
                .IsRequired();

            builder.Property(c => c.Cpf)
                .HasColumnType(varchar)
                .IsRequired();

            builder.Property(c => c.Telefone)
                .HasColumnType(varchar);

            builder.Property(c => c.Endereco)
                .HasColumnType(varchar);

            builder.Property(c => c.Observacao)
                .HasColumnType(varchar);

            builder.Property(p => p.DataInativacao)
            .HasColumnType("timestamp");

            builder.HasOne(c => c.Usuario)
                .WithMany(u => u.Clientes)
                .HasForeignKey(c => c.UsuarioId);

            builder.HasMany(c => c.Produtos)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.ClienteId);

            builder.HasQueryFilter(c => c.DataInativacao == null);
        }
    }
}
