﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ms_spa.Api.Data;

#nullable disable

namespace ms_spa.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240606203726_initialMigration")]
    partial class initialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ms_spa.Api.Domain.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("cliente", (string)null);
                });

            modelBuilder.Entity("ms_spa.Api.Domain.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<int>("QuantidadeEstoque")
                        .HasColumnType("int");

                    b.Property<double>("ValorCusto")
                        .HasColumnType("double precision");

                    b.Property<double>("ValorVenda")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("produto", (string)null);
                });

            modelBuilder.Entity("ms_spa.Api.Domain.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp");

                    b.Property<DateTime?>("DataInativacao")
                        .HasColumnType("timestamp");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Perfil")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("usuario", (string)null);
                });

            modelBuilder.Entity("ms_spa.Api.Domain.Models.Cliente", b =>
                {
                    b.HasOne("ms_spa.Api.Domain.Models.Usuario", "Usuario")
                        .WithMany("Clientes")
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ms_spa.Api.Domain.Models.Produto", b =>
                {
                    b.HasOne("ms_spa.Api.Domain.Models.Cliente", "Cliente")
                        .WithMany("Produtos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ms_spa.Api.Domain.Models.Cliente", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("ms_spa.Api.Domain.Models.Usuario", b =>
                {
                    b.Navigation("Clientes");
                });
#pragma warning restore 612, 618
        }
    }
}
