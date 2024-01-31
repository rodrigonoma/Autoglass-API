﻿// <auto-generated />
using System;
using Autoglass.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Autoglass.Infrastructure.Migrations
{
    [DbContext(typeof(MeuDbContext))]
    [Migration("20240131011207_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Autoglass.Domain.Entities.Produto", b =>
                {
                    b.Property<int>("CodigoProduto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CNPJFornecedor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CodigoFornecedor")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataFabricacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataValidade")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DescricaoFornecedor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DescricaoProduto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SituacaoProduto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CodigoProduto");

                    b.ToTable("Produtos", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
