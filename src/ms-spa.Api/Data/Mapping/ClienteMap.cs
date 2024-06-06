using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ms_spa.Api.Domain.Models;

namespace ms_spa.Api.Data.Mapping
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("cliente")
              .HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasColumnType("VARCHAR")
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType("VARCHAR");

            builder.Property(c => c.Cpf)
                .HasColumnType("VARCHAR");

            builder.Property(c => c.Telefone)
                .HasColumnType("VARCHAR");

            builder.Property(c => c.Endereco)
                .HasColumnType("VARCHAR");

            builder.Property(c => c.Observacao)
                .HasColumnType("VARCHAR");

            builder.HasOne(c => c.Usuario)
                .WithMany(u => u.Clientes)
                .HasForeignKey(c => c.UsuarioId);

            builder.HasMany(c => c.Produtos)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.ClienteId);
        }
    }
}
