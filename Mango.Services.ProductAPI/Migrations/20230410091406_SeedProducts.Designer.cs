﻿// <auto-generated />
using Mango.Services.ProductAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mango.Services.ProductAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230410091406_SeedProducts")]
    partial class SeedProducts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.2.23128.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mango.Services.ProductAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryName = "Appetizer",
                            Description = "Kebap ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.",
                            ImageUrl = "https://mediastormango.blob.core.windows.net/mango/kebap.jpg",
                            Name = "Kebap",
                            Price = 25.0
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryName = "Appetizer",
                            Description = "Iskender ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.",
                            ImageUrl = "https://mediastormango.blob.core.windows.net/mango/iskender.jpg",
                            Name = "Iskender",
                            Price = 34.990000000000002
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryName = "Appetizer",
                            Description = "Doner ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.",
                            ImageUrl = "https://mediastormango.blob.core.windows.net/mango/doner.jpg",
                            Name = "Doner",
                            Price = 15.99
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryName = "Appetizer",
                            Description = "Pide ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.",
                            ImageUrl = "https://mediastormango.blob.core.windows.net/mango/pide.jpg",
                            Name = "Pide",
                            Price = 13.85
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryName = "Dessert",
                            Description = "Baklava ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.",
                            ImageUrl = "https://mediastormango.blob.core.windows.net/mango/baklava.jpg",
                            Name = "Baklava",
                            Price = 10.35
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryName = "Dessert",
                            Description = "Donut ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.",
                            ImageUrl = "https://mediastormango.blob.core.windows.net/mango/donut.jpg",
                            Name = "Donut",
                            Price = 8.8800000000000008
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryName = "Entree",
                            Description = "Atom ipsum dolor sit amet, consectetur adipiscing elit. Ut condimentum auctor diam, et suscipit sem. Proin ut est dictum, dapibus arcu quis, porta lectus. Maecenas molestie sit amet lorem in auctor. Praesent nec libero quam. Nullam scelerisque enim dui, non vehicula ligula faucibus non. Etiam rutrum vehicula dui in ultricies.",
                            ImageUrl = "https://mediastormango.blob.core.windows.net/mango/atom.jpg",
                            Name = "Atom",
                            Price = 8.8800000000000008
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
