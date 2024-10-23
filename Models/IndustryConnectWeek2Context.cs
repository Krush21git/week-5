using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IndustryConnect_Week5_WebApi.Models;

public partial class IndustryConnectWeek2Context : DbContext
{
    public IndustryConnectWeek2Context()
    {
    }

    public IndustryConnectWeek2Context(DbContextOptions<IndustryConnectWeek2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerSale> CustomerSales { get; set; }

    public virtual DbSet<CustomerSaleView> CustomerSaleViews { get; set; }

    public virtual DbSet<CustomerSaleView1> CustomerSaleView1s { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Store1> Store1s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=KRUTIKA\\MSSQLSERVER01;Initial Catalog=IndustryConnectWeek2;Integrated Security=True;TrustServerCertificate=Yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(40);
        });

        modelBuilder.Entity<CustomerSale>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CustomerSales");

            entity.Property(e => e.CustomerId).HasColumnName("Customer Id");
            entity.Property(e => e.DateSold).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(40);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.TotalPurchases)
                .HasColumnType("money")
                .HasColumnName("Total Purchases");
        });

        modelBuilder.Entity<CustomerSaleView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CustomerSaleView");

            entity.Property(e => e.DateSold).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.FullName).HasMaxLength(71);
            entity.Property(e => e.LastName).HasMaxLength(40);
        });

        modelBuilder.Entity<CustomerSaleView1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CustomerSaleView1");

            entity.Property(e => e.DateSold).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.FullName).HasMaxLength(71);
            entity.Property(e => e.LastName).HasMaxLength(40);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.ToTable("Sale");

            entity.Property(e => e.DateSold).HasColumnType("datetime");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Sale_Customer");

            entity.HasOne(d => d.Product).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Sale_Product");

            entity.HasOne(d => d.Store).WithMany(p => p.Sales)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Sale_Store");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Store__3B82F0E1EC5649E2");

            entity.ToTable("Store");

            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Store1>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Store1__3B82F0E1DEE223BF");

            entity.ToTable("Store1");

            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
