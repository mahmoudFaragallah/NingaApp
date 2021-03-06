﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NingaApp.Data;

namespace NingaApp.Data.Migrations
{
    [DbContext(typeof(NingaContext))]
    partial class NingaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NingaApp.Domain.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Battles");
                });

            modelBuilder.Entity("NingaApp.Domain.Ninga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ningas");
                });

            modelBuilder.Entity("NingaApp.Domain.NingaBattle", b =>
                {
                    b.Property<int>("NingaId")
                        .HasColumnType("int");

                    b.Property<int>("BattleId")
                        .HasColumnType("int");

                    b.HasKey("NingaId", "BattleId");

                    b.HasIndex("BattleId");

                    b.ToTable("NingaBattle");
                });

            modelBuilder.Entity("NingaApp.Domain.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NingaId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NingaId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("NingaApp.Domain.SecretIdentity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NingaId")
                        .HasColumnType("int");

                    b.Property<string>("RealName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NingaId")
                        .IsUnique();

                    b.ToTable("SecretIdentity");
                });

            modelBuilder.Entity("NingaApp.Domain.NingaBattle", b =>
                {
                    b.HasOne("NingaApp.Domain.Battle", "Battle")
                        .WithMany("NingaBattles")
                        .HasForeignKey("BattleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NingaApp.Domain.Ninga", "Ninga")
                        .WithMany("NingaBattles")
                        .HasForeignKey("NingaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NingaApp.Domain.Quote", b =>
                {
                    b.HasOne("NingaApp.Domain.Ninga", "Ninga")
                        .WithMany("Quotes")
                        .HasForeignKey("NingaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NingaApp.Domain.SecretIdentity", b =>
                {
                    b.HasOne("NingaApp.Domain.Ninga", null)
                        .WithOne("SecretIdentity")
                        .HasForeignKey("NingaApp.Domain.SecretIdentity", "NingaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
