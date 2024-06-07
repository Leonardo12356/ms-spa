using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ms_spa.Api.Domain.Models;

namespace ms_spa.Api.Data.Mapping
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("produto")
                .HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasColumnType("VARCHAR")
                .IsRequired();

            builder.Property(p => p.QuantidadeEstoque)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.ValorCusto)
                .HasColumnType("double precision")
                .IsRequired();

            builder.Property(p => p.ValorVenda)
                .HasColumnType("double precision")
                .IsRequired();

            builder.Property(p => p.Observacao)
                .HasColumnType("VARCHAR");

            builder.Property(p => p.DataCadastro)
                .HasColumnType("timestamp")
                .IsRequired();

            builder.HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.ClienteId);

            builder.HasQueryFilter(p => p.Cliente.DataInativacao == null);
        }
    }
}