﻿// <auto-generated />
using System;
using CleanArchitectureApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleanArchitectureApi.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250330202033_OutboxChanges")]
    partial class OutboxChanges
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CleanArchitectureApi.Domain.Customers.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Balance")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CleanArchitectureApi.Domain.InvoiceItems.Entities.InvoiceItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<decimal>("SellPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceItems");
                });

            modelBuilder.Entity("CleanArchitectureApi.Domain.Invoices.Entities.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PoNumber")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<decimal>("TotalBalance")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("CleanArchitectureApi.Domain.Products.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CleanArchitectureApi.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccuredOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages", (string)null);
                });

            modelBuilder.Entity("CleanArchitectureApi.Domain.Customers.Entities.Customer", b =>
                {
                    b.OwnsOne("CleanArchitectureApi.Domain.Customers.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("AddressLine1")
                                .IsRequired()
                                .HasMaxLength(40)
                                .HasColumnType("nvarchar(40)");

                            b1.Property<string>("AddressLine2")
                                .HasMaxLength(40)
                                .HasColumnType("nvarchar(40)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)");

                            b1.Property<string>("County")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)");

                            b1.Property<string>("Postcode")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.Property<string>("Town")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("CleanArchitectureApi.Domain.InvoiceItems.Entities.InvoiceItem", b =>
                {
                    b.HasOne("CleanArchitectureApi.Domain.Invoices.Entities.Invoice", "Invoice")
                        .WithMany("PurchasedProducts")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("CleanArchitectureApi.Domain.Invoices.Entities.Invoice", b =>
                {
                    b.HasOne("CleanArchitectureApi.Domain.Customers.Entities.Customer", "Customer")
                        .WithMany("Invoices")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CleanArchitectureApi.Domain.Customers.Entities.Customer", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("CleanArchitectureApi.Domain.Invoices.Entities.Invoice", b =>
                {
                    b.Navigation("PurchasedProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
