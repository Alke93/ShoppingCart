﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingCart.DataAccessLayer.Db;

#nullable disable

namespace ShoppingCart.DataAccessLayer.Migrations
{
    [DbContext(typeof(GenericDbContext))]
    [Migration("20230117150710_Data")]
    partial class Data
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShoppingCart.Entities.Model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float")
                        .HasColumnOrder(3);

                    b.Property<string>("CustomerAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnOrder(6);

                    b.Property<string>("CustomerPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnOrder(7);

                    b.Property<double>("Discount")
                        .HasColumnType("float")
                        .HasColumnOrder(4);

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<double>("Sum")
                        .HasColumnType("float")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ShoppingCart.Entities.Model.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnOrder(2);

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnOrder(5);

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnOrder(3);

                    b.Property<int>("ReservedQuantity")
                        .HasColumnType("int")
                        .HasColumnOrder(4);

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ShoppingCart.Entities.Model.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit")
                        .HasColumnOrder(3);

                    b.HasKey("Id");

                    b.ToTable("ShoppingCart");
                });

            modelBuilder.Entity("ShoppingCart.Entities.Model.ShoppingItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnOrder(3);

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnOrder(4);

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float")
                        .HasColumnOrder(6);

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingItem");
                });

            modelBuilder.Entity("ShoppingCart.Entities.Model.Order", b =>
                {
                    b.HasOne("ShoppingCart.Entities.Model.ShoppingCart", "ShoppingCart")
                        .WithMany()
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("ShoppingCart.Entities.Model.ShoppingItem", b =>
                {
                    b.HasOne("ShoppingCart.Entities.Model.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingCart.Entities.Model.ShoppingCart", "ShoppingCart")
                        .WithMany("ShoppingItems")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("ShoppingCart.Entities.Model.ShoppingCart", b =>
                {
                    b.Navigation("ShoppingItems");
                });
#pragma warning restore 612, 618
        }
    }
}