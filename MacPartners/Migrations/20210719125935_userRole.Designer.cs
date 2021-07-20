﻿// <auto-generated />
using System;
using MacPartners.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MacPartners.Migrations
{
    [DbContext(typeof(MacPartnersContext))]
    [Migration("20210719125935_userRole")]
    partial class userRole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
