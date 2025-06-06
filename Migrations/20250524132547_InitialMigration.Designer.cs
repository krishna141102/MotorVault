﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MotorVault.Data;

#nullable disable

namespace MotorVault.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250524132547_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MotorVault.Model.Domain.Brand", b =>
                {
                    b.Property<string>("BrandName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("BrandName");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("MotorVault.Model.Domain.CarModel", b =>
                {
                    b.Property<Guid>("CarModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CarTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EngineType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("HorsePower")
                        .HasColumnType("int");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.HasKey("CarModelId");

                    b.HasIndex("CarTypeId");

                    b.ToTable("CarModels");
                });

            modelBuilder.Entity("MotorVault.Model.Domain.CarType", b =>
                {
                    b.Property<Guid>("CarTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CarTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CarTypeId");

                    b.HasIndex("BrandName");

                    b.ToTable("CarTypes");
                });

            modelBuilder.Entity("MotorVault.Model.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("MotorVault.Model.Domain.Vehicle", b =>
                {
                    b.Property<Guid>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CarModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CarTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FuelType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TransmissionType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("VehicleId");

                    b.HasIndex("CarModelId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("MotorVault.Model.Domain.CarModel", b =>
                {
                    b.HasOne("MotorVault.Model.Domain.CarType", "CarType")
                        .WithMany("CarModels")
                        .HasForeignKey("CarTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarType");
                });

            modelBuilder.Entity("MotorVault.Model.Domain.CarType", b =>
                {
                    b.HasOne("MotorVault.Model.Domain.Brand", "Brand")
                        .WithMany("CarTypes")
                        .HasForeignKey("BrandName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("MotorVault.Model.Domain.Vehicle", b =>
                {
                    b.HasOne("MotorVault.Model.Domain.CarModel", "CarModel")
                        .WithMany("Vehicles")
                        .HasForeignKey("CarModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarModel");
                });

            modelBuilder.Entity("MotorVault.Model.Domain.Brand", b =>
                {
                    b.Navigation("CarTypes");
                });

            modelBuilder.Entity("MotorVault.Model.Domain.CarModel", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("MotorVault.Model.Domain.CarType", b =>
                {
                    b.Navigation("CarModels");
                });
#pragma warning restore 612, 618
        }
    }
}
