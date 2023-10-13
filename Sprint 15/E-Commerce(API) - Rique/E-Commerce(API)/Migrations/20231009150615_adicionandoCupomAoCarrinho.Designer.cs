﻿// <auto-generated />
using System;
using E_Commerce_API_.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace E_Commerce_API_.Migrations
{
    [DbContext(typeof(ECommerceContext))]
    [Migration("20231009150615_adicionandoCupomAoCarrinho")]
    partial class adicionandoCupomAoCarrinho
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("E_Commerce_API_.Models.CarrinhoDeCompras", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Bairro")
                        .HasColumnType("longtext");

                    b.Property<string>("CEP")
                        .HasColumnType("longtext");

                    b.Property<string>("Cidade")
                        .HasColumnType("longtext");

                    b.Property<string>("Complemento")
                        .HasColumnType("longtext");

                    b.Property<string>("Cupom")
                        .HasColumnType("longtext");

                    b.Property<string>("Logradouro")
                        .HasColumnType("longtext");

                    b.Property<uint?>("Numero")
                        .HasColumnType("int unsigned");

                    b.Property<string>("UF")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CarrinhosDeCompras");
                });

            modelBuilder.Entity("E_Commerce_API_.Models.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataEHoraCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataEHoraModificacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("E_Commerce_API_.Models.CentroDeDistribuicao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime>("DataEHoraCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataEHoraModificacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<uint>("Numero")
                        .HasColumnType("int unsigned");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UF")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.ToTable("CentrosDeDistribuicao");
                });

            modelBuilder.Entity("E_Commerce_API_.Models.CupomDeDesconto", b =>
                {
                    b.Property<string>("Cupom")
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("SetorDeDesconto")
                        .HasColumnType("char(36)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("TipoDeDesconto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Uso")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("ValorDoDesconto")
                        .HasColumnType("double");

                    b.HasKey("Cupom");

                    b.ToTable("CuponsDeDesconto");
                });

            modelBuilder.Entity("E_Commerce_API_.Models.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<double>("Altura")
                        .HasColumnType("double");

                    b.Property<Guid>("CentroDeDistribuicaoId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Comprimento")
                        .HasColumnType("double");

                    b.Property<DateTime>("DataEHoraCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataEHoraModificacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<double>("Largura")
                        .HasColumnType("double");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<double>("Peso")
                        .HasColumnType("double");

                    b.Property<int>("QuantidadeEmEstoque")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("SubcategoriaId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Valor")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("CentroDeDistribuicaoId");

                    b.HasIndex("SubcategoriaId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("E_Commerce_API_.Models.ProdutoNoCarrinho", b =>
                {
                    b.Property<Guid>("CarrinhoDeComprasId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("char(36)");

                    b.Property<uint>("Quantidade")
                        .HasColumnType("int unsigned");

                    b.Property<double>("ValorNoCarrinho")
                        .HasColumnType("double");

                    b.HasKey("CarrinhoDeComprasId", "ProdutoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ProdutosNoCarrinho");
                });

            modelBuilder.Entity("E_Commerce_API_.Models.Subcategoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataEHoraCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataEHoraModificacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Subcategorias");
                });

            modelBuilder.Entity("E_Commerce_API_.Models.Produto", b =>
                {
                    b.HasOne("E_Commerce_API_.Models.CentroDeDistribuicao", "CentroDeDistribuicao")
                        .WithMany("Produtos")
                        .HasForeignKey("CentroDeDistribuicaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_Commerce_API_.Models.Subcategoria", "Subcategoria")
                        .WithMany("Produtos")
                        .HasForeignKey("SubcategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CentroDeDistribuicao");

                    b.Navigation("Subcategoria");
                });

            modelBuilder.Entity("E_Commerce_API_.Models.ProdutoNoCarrinho", b =>
                {
                    b.HasOne("E_Commerce_API_.Models.CarrinhoDeCompras", "CarrinhoDeCompras")
                        .WithMany("Produtos")
                        .HasForeignKey("CarrinhoDeComprasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_Commerce_API_.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarrinhoDeCompras");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("E_Commerce_API_.Models.Subcategoria", b =>
                {
                    b.HasOne("E_Commerce_API_.Models.Categoria", "Categoria")
                        .WithMany("Subcategorias")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("E_Commerce_API_.Models.CarrinhoDeCompras", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("E_Commerce_API_.Models.Categoria", b =>
                {
                    b.Navigation("Subcategorias");
                });

            modelBuilder.Entity("E_Commerce_API_.Models.CentroDeDistribuicao", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("E_Commerce_API_.Models.Subcategoria", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
