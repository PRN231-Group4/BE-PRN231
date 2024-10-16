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
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBDefault"));
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
                    .HasConstraintName("FK__Account__Role_Id__48CFD27E");
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
                    .HasConstraintName("FK__Wine__Category_I__49C3F6B7");
            });

            modelBuilder.Entity<WineBatch>(entity =>
            {
                entity.HasKey(e => e.BatchId)
                    .HasName("PK__WineBatc__28E47C736E8C3D1C");

                entity.ToTable("WineBatch");

                entity.Property(e => e.BatchId).HasColumnName("Batch_Id");

                entity.Property(e => e.BatchNumber).HasMaxLength(50);

                entity.Property(e => e.ImportDate).HasColumnType("datetime");

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
                    .HasName("PK__WineChec__7063BF732DEFF8B3");

                entity.ToTable("WineCheck");

                entity.Property(e => e.CheckId).HasColumnName("Check_Id");

                entity.Property(e => e.BatchId).HasColumnName("Batch_Id");

                entity.Property(e => e.CheckDate).HasColumnType("datetime");

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

                entity.HasOne(d => d.Inspector)
                    .WithMany(p => p.WineChecks)
                    .HasForeignKey(d => d.InspectorId)
                    .HasConstraintName("FK_WineCheck_Account");

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
                    .HasName("PK__WineRequ__E9C5B3738B07BDD0");

                entity.ToTable("WineRequest");

                entity.Property(e => e.RequestId).HasColumnName("Request_Id");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ManagerId).HasColumnName("Manager_Id");

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");

                entity.Property(e => e.WineId).HasColumnName("Wine_Id");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.WineRequests)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_WineRequest_Account");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.WineRequests)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_WineRequest_Supplier");
            });

            modelBuilder.Entity<WineStorageLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__WineStor__D2BA00E2DF8A2B25");

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
                    .HasName("PK__WineTran__9A8D56051EECF152");

                entity.ToTable("WineTransaction");

                entity.Property(e => e.TransactionId).HasColumnName("Transaction_Id");

                entity.Property(e => e.BatchId).HasColumnName("Batch_Id");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.InspectorId).HasColumnName("Inspector_Id");

                entity.Property(e => e.LocationId).HasColumnName("Location_Id");

                entity.Property(e => e.TransDate).HasColumnType("datetime");

                entity.Property(e => e.TransType).HasMaxLength(50);

                entity.Property(e => e.WineId).HasColumnName("Wine_Id");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.WineTransactions)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK__WineTrans__Batch__3F466844");

                entity.HasOne(d => d.Inspector)
                    .WithMany(p => p.WineTransactions)
                    .HasForeignKey(d => d.InspectorId)
                    .HasConstraintName("FK_WineTransaction_Account");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.WineTransactions)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__WineTrans__Locat__403A8C7D");

                entity.HasOne(d => d.Wine)
                    .WithMany(p => p.WineTransactions)
                    .HasForeignKey(d => d.WineId)
                    .HasConstraintName("FK__WineTrans__Wine___412EB0B6");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
