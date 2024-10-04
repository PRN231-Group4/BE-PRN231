using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Models
{
    public partial class WineManagementSystemContext : DbContext
    {
        public WineManagementSystemContext()
        {
        }

        public WineManagementSystemContext(DbContextOptions<WineManagementSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<Wine> Wines { get; set; } = null!;
        public virtual DbSet<WineBatch> WineBatches { get; set; } = null!;
        public virtual DbSet<WineCheck> WineChecks { get; set; } = null!;
        public virtual DbSet<WineRequest> WineRequests { get; set; } = null!;
        public virtual DbSet<WineStorageLocation> WineStorageLocations { get; set; } = null!;
        public virtual DbSet<WineTransaction> WineTransactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            }
        }

        private string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true).Build();
            return configuration["ConnectionStrings:DBDefault"];
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasColumnName("Account_Id");

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(100);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Account__Role_Id__35BCFE0A");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Wine>(entity =>
            {
                entity.ToTable("Wine");

                entity.Property(e => e.WineId).HasColumnName("Wine_Id");

                entity.Property(e => e.AlcContent).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Origin).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Volume).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Wines)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Wine__Category_I__36B12243");
            });

            modelBuilder.Entity<WineBatch>(entity =>
            {
                entity.HasKey(e => e.BatchId)
                    .HasName("PK__WineBatc__28E47C733802CCA6");

                entity.ToTable("WineBatch");

                entity.Property(e => e.BatchId).HasColumnName("Batch_Id");

                entity.Property(e => e.BatchNumber).HasMaxLength(50);

                entity.Property(e => e.ImportDate).HasColumnType("date");

                entity.Property(e => e.RequestId).HasColumnName("Request_Id");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.WineId).HasColumnName("Wine_Id");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.WineBatches)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__WineBatch__Reque__37A5467C");

                entity.HasOne(d => d.Wine)
                    .WithMany(p => p.WineBatches)
                    .HasForeignKey(d => d.WineId)
                    .HasConstraintName("FK__WineBatch__Wine___38996AB5");
            });

            modelBuilder.Entity<WineCheck>(entity =>
            {
                entity.HasKey(e => e.CheckId)
                    .HasName("PK__WineChec__7063BF73063BC439");

                entity.ToTable("WineCheck");

                entity.Property(e => e.CheckId).HasColumnName("Check_Id");

                entity.Property(e => e.BatchId).HasColumnName("Batch_Id");

                entity.Property(e => e.CheckDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.InspectorId).HasColumnName("Inspector_Id");

                entity.Property(e => e.RequestId).HasColumnName("Request_Id");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.WineId).HasColumnName("Wine_Id");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.WineChecks)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK__WineCheck__Batch__398D8EEE");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.WineChecks)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__WineCheck__Reque__3A81B327");

                entity.HasOne(d => d.Wine)
                    .WithMany(p => p.WineChecks)
                    .HasForeignKey(d => d.WineId)
                    .HasConstraintName("FK__WineCheck__Wine___3B75D760");
            });

            modelBuilder.Entity<WineRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__WineRequ__E9C5B373ED624C07");

                entity.ToTable("WineRequest");

                entity.Property(e => e.RequestId).HasColumnName("Request_Id");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ManagerId).HasColumnName("Manager_Id");

                entity.Property(e => e.RequestData).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");

                entity.Property(e => e.WineId).HasColumnName("Wine_Id");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.WineRequests)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_WineRequest_Supplier");
            });

            modelBuilder.Entity<WineStorageLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__WineStor__D2BA00E2BDF01BA2");

                entity.ToTable("WineStorageLocation");

                entity.Property(e => e.LocationId).HasColumnName("Location_Id");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ShelfCode).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Zone).HasMaxLength(50);
            });

            modelBuilder.Entity<WineTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__WineTran__9A8D5605F92D4D4C");

                entity.ToTable("WineTransaction");

                entity.Property(e => e.TransactionId).HasColumnName("Transaction_Id");

                entity.Property(e => e.BatchId).HasColumnName("Batch_Id");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.InspectorId).HasColumnName("Inspector_Id");

                entity.Property(e => e.LocationId).HasColumnName("Location_Id");

                entity.Property(e => e.TransDate).HasColumnType("date");

                entity.Property(e => e.TransType).HasMaxLength(50);

                entity.Property(e => e.WineId).HasColumnName("Wine_Id");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.WineTransactions)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK__WineTrans__Batch__3C69FB99");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.WineTransactions)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__WineTrans__Locat__3D5E1FD2");

                entity.HasOne(d => d.Wine)
                    .WithMany(p => p.WineTransactions)
                    .HasForeignKey(d => d.WineId)
                    .HasConstraintName("FK__WineTrans__Wine___3E52440B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
