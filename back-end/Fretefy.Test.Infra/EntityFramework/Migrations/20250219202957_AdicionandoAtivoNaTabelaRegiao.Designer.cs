﻿// <auto-generated />
using System;
using Fretefy.Test.Infra.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fretefy.Test.Infra.EntityFramework.Migrations
{
    [DbContext(typeof(TestDbContext))]
    [Migration("20250219202957_AdicionandoAtivoNaTabelaRegiao")]
    partial class AdicionandoAtivoNaTabelaRegiao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Fretefy.Test.Domain.Entities.Cidade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("TEXT");

                    b.Property<string>("UF")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cidade");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f399761f-600f-4616-9021-00739846110c"),
                            Nome = "Rio Branco",
                            UF = "AC"
                        },
                        new
                        {
                            Id = new Guid("b847fecb-a9da-4138-852a-a3a88085baea"),
                            Nome = "Maceió",
                            UF = "AL"
                        },
                        new
                        {
                            Id = new Guid("445066f0-ca54-4315-90d0-224abb29b0a7"),
                            Nome = "Macapá",
                            UF = "AP"
                        },
                        new
                        {
                            Id = new Guid("236f52f2-9705-4c35-b75b-9fd0572e02f0"),
                            Nome = "Manaus",
                            UF = "AM"
                        },
                        new
                        {
                            Id = new Guid("bdd23b70-787e-44aa-97ab-a04bbf5b9b1a"),
                            Nome = "Salvador",
                            UF = "BA"
                        },
                        new
                        {
                            Id = new Guid("3b65ac5c-6230-4220-82f5-b7144f84c4ec"),
                            Nome = "Fortaleza",
                            UF = " CE"
                        },
                        new
                        {
                            Id = new Guid("752ad469-4686-43a9-a1a3-d8b4e031f065"),
                            Nome = "Brasília",
                            UF = "DF"
                        },
                        new
                        {
                            Id = new Guid("5609137d-60a4-42c1-80cd-95442c8e4cd3"),
                            Nome = "Vitória",
                            UF = "ES"
                        },
                        new
                        {
                            Id = new Guid("ddac05e4-f08e-4ded-b97e-fe1e120d127b"),
                            Nome = "Goiânia",
                            UF = "GO"
                        },
                        new
                        {
                            Id = new Guid("d901f51d-f512-4e38-9013-530e07e2dc14"),
                            Nome = "São Luís",
                            UF = "MA"
                        },
                        new
                        {
                            Id = new Guid("2b2c5779-a72c-4f7d-af4e-9ea9e0dc1c77"),
                            Nome = "Cuiabá",
                            UF = "MT"
                        },
                        new
                        {
                            Id = new Guid("6c59f6a1-5d6f-41e0-8480-d428df67e941"),
                            Nome = "Campo Grande",
                            UF = "MS"
                        },
                        new
                        {
                            Id = new Guid("a08eda78-899e-401d-a4a9-c5f8e6e4fe6b"),
                            Nome = "Belo Horizonte",
                            UF = "MG"
                        },
                        new
                        {
                            Id = new Guid("5361609b-1f0f-4f3a-923d-5acbfffe322a"),
                            Nome = "Belém",
                            UF = "PA"
                        },
                        new
                        {
                            Id = new Guid("cf8aba20-291e-4c70-844f-af48bf4a5aa3"),
                            Nome = "João Pessoa",
                            UF = "PB"
                        },
                        new
                        {
                            Id = new Guid("baf219fe-e907-4b6e-80ea-3eb6e11e98f0"),
                            Nome = "Curitiba",
                            UF = "PR"
                        },
                        new
                        {
                            Id = new Guid("02f4cc32-eaf2-47da-a8e7-95adb3567899"),
                            Nome = "Recife",
                            UF = "PE"
                        },
                        new
                        {
                            Id = new Guid("35963c8a-8381-4517-bdcc-627312fec76a"),
                            Nome = "Teresina",
                            UF = "PI"
                        },
                        new
                        {
                            Id = new Guid("0a8e2ccf-6846-4e11-aafe-455b2a6c88cb"),
                            Nome = "Rio de Janeiro",
                            UF = "RJ"
                        },
                        new
                        {
                            Id = new Guid("b67794c3-3ef0-407f-ab96-7dbce67a6756"),
                            Nome = "Natal",
                            UF = "RN"
                        },
                        new
                        {
                            Id = new Guid("9b9e9b69-475b-4ba8-86d1-8369d08fdd53"),
                            Nome = "Porto Alegre",
                            UF = "RS"
                        },
                        new
                        {
                            Id = new Guid("7fec79b1-6fc1-45d9-a836-67f7ff36d30c"),
                            Nome = "Porto Velho",
                            UF = "RO"
                        },
                        new
                        {
                            Id = new Guid("7ebef894-0cab-46f4-b9a5-ef1d12df17f4"),
                            Nome = "Boa Vista",
                            UF = "RR"
                        },
                        new
                        {
                            Id = new Guid("0ef783a5-79ef-4e47-98f6-11111a8ca395"),
                            Nome = "Florianópolis",
                            UF = "SC"
                        },
                        new
                        {
                            Id = new Guid("666caac1-443a-4a18-8ab3-bdc18ec063d6"),
                            Nome = "São Paulo",
                            UF = "SP"
                        },
                        new
                        {
                            Id = new Guid("5e14833a-d2bf-4e1e-a0ee-df8d885d85f8"),
                            Nome = "Aracaju",
                            UF = "SE"
                        },
                        new
                        {
                            Id = new Guid("d06c6968-c64b-420f-a9ec-bb8769e7b00c"),
                            Nome = "Palmas",
                            UF = "TO"
                        });
                });

            modelBuilder.Entity("Fretefy.Test.Domain.Entities.Regiao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Regiao");
                });

            modelBuilder.Entity("Fretefy.Test.Domain.Entities.RegiaoCidade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CidadeId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RegiaoId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CidadeId");

                    b.HasIndex("RegiaoId");

                    b.ToTable("RegiaoCidade");
                });

            modelBuilder.Entity("Fretefy.Test.Domain.Entities.RegiaoCidade", b =>
                {
                    b.HasOne("Fretefy.Test.Domain.Entities.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fretefy.Test.Domain.Entities.Regiao", "Regiao")
                        .WithMany("RegiaoCidades")
                        .HasForeignKey("RegiaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cidade");

                    b.Navigation("Regiao");
                });

            modelBuilder.Entity("Fretefy.Test.Domain.Entities.Regiao", b =>
                {
                    b.Navigation("RegiaoCidades");
                });
#pragma warning restore 612, 618
        }
    }
}
