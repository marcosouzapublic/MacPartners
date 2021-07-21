﻿// <auto-generated />
using System;
using MacPartners.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MacPartners.Migrations
{
    [DbContext(typeof(MacPartnersContext))]
    partial class MacPartnersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MacPartners.Domain.Models.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PartnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("PartnerId");

                    b.HasIndex("PersonId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BlockedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.Partner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BlockedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.ValueObjects.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Complement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ZipCodeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ZipCodeId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.ValueObjects.Cnpj", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cnpj");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.ValueObjects.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CnpjId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PhoneId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TradeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CnpjId");

                    b.HasIndex("PhoneId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.ValueObjects.Cpf", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cpf");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.ValueObjects.Email", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAdress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Email");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.ValueObjects.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CpfId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EmailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PhoneId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CpfId");

                    b.HasIndex("EmailId");

                    b.HasIndex("PhoneId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.ValueObjects.Phone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AreaCode")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Phone");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.ValueObjects.ZipCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ZipCode");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.Entities.Customer", b =>
                {
                    b.HasOne("MacPartners.Domain.Models.ValueObjects.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("MacPartners.Domain.Models.ValueObjects.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("MacPartners.Domain.Models.Partner", "Partner")
                        .WithMany()
                        .HasForeignKey("PartnerId");

                    b.HasOne("MacPartners.Domain.Models.ValueObjects.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.Navigation("Address");

                    b.Navigation("Company");

                    b.Navigation("Partner");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.Entities.User", b =>
                {
                    b.HasOne("MacPartners.Domain.Models.ValueObjects.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.Partner", b =>
                {
                    b.HasOne("MacPartners.Domain.Models.ValueObjects.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.ValueObjects.Address", b =>
                {
                    b.HasOne("MacPartners.Domain.Models.ValueObjects.ZipCode", "ZipCode")
                        .WithMany()
                        .HasForeignKey("ZipCodeId");

                    b.Navigation("ZipCode");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.ValueObjects.Company", b =>
                {
                    b.HasOne("MacPartners.Domain.Models.ValueObjects.Cnpj", "Cnpj")
                        .WithMany()
                        .HasForeignKey("CnpjId");

                    b.HasOne("MacPartners.Domain.Models.ValueObjects.Phone", "Phone")
                        .WithMany()
                        .HasForeignKey("PhoneId");

                    b.Navigation("Cnpj");

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("MacPartners.Domain.Models.ValueObjects.Person", b =>
                {
                    b.HasOne("MacPartners.Domain.Models.ValueObjects.Cpf", "Cpf")
                        .WithMany()
                        .HasForeignKey("CpfId");

                    b.HasOne("MacPartners.Domain.Models.ValueObjects.Email", "Email")
                        .WithMany()
                        .HasForeignKey("EmailId");

                    b.HasOne("MacPartners.Domain.Models.ValueObjects.Phone", "Phone")
                        .WithMany()
                        .HasForeignKey("PhoneId");

                    b.Navigation("Cpf");

                    b.Navigation("Email");

                    b.Navigation("Phone");
                });
#pragma warning restore 612, 618
        }
    }
}
