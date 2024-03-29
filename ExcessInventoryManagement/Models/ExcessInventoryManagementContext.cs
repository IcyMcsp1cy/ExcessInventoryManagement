﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ExcessInventoryManagement.Models;

public partial class ExcessInventoryManagementContext : DbContext
{
    public ExcessInventoryManagementContext()
    {
    }

    public ExcessInventoryManagementContext(DbContextOptions<ExcessInventoryManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<MarkdownPlan> MarkdownPlans { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Sales> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToTable("Inventory");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MarkdownPlan>(entity =>
        {
            entity.ToTable("MarkdownPlan");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.FinalPriceReduction).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.InitialPriceReduction).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.MidwayPriceReduction).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sales>(entity =>
        {
            entity.ToTable("Sales");

            entity.Property(e => e.SalesData)
                .HasMaxLength(1000)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
