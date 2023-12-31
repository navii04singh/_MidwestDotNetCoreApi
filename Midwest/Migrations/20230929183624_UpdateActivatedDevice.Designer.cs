﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Midwest.Data;

#nullable disable

namespace Midwest.Migrations
{
    [DbContext(typeof(MidWestDBContext))]
    [Migration("20230929183624_UpdateActivatedDevice")]
    partial class UpdateActivatedDevice
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Midwest.Models.Domain.ActivatedDevice", b =>
                {
                    b.Property<int>("ActivatedDeviceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActivatedDeviceID"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<string>("DeviceID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LicenseKey")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ActivatedDeviceID");

                    b.HasIndex("LicenseKey");

                    b.ToTable("ActivatedDevice");
                });

            modelBuilder.Entity("Midwest.Models.Domain.tblClients", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientID"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LicenseKey")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NoOfMachines")
                        .HasColumnType("int");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientID");

                    b.ToTable("tblClients");
                });

            modelBuilder.Entity("tblLicenses", b =>
                {
                    b.Property<Guid>("LicenseKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<int>("CurrentUnits")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaxUnits")
                        .HasColumnType("int");

                    b.HasKey("LicenseKey");

                    b.HasIndex("ClientID");

                    b.ToTable("tblLicenses");
                });

            modelBuilder.Entity("Midwest.Models.Domain.ActivatedDevice", b =>
                {
                    b.HasOne("tblLicenses", "License")
                        .WithMany("ActivatedDevices")
                        .HasForeignKey("LicenseKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("License");
                });

            modelBuilder.Entity("tblLicenses", b =>
                {
                    b.HasOne("Midwest.Models.Domain.tblClients", "Client")
                        .WithMany("Licenses")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Midwest.Models.Domain.tblClients", b =>
                {
                    b.Navigation("Licenses");
                });

            modelBuilder.Entity("tblLicenses", b =>
                {
                    b.Navigation("ActivatedDevices");
                });
#pragma warning restore 612, 618
        }
    }
}
