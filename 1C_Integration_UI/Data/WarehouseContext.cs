using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.IO;
using _1C_Integration_UI.Models;

namespace _1C_Integration_UI.Data
{
    public class WarehouseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Counterparty> Counterparties { get; set; }
        public WarehouseContext()
        {
            // Ensure database and tables are created when context is first used
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WarehouseDB_Test1;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasIndex(p => p.Article).IsUnique();
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.BasePrice).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Number).IsRequired();
                entity.Property(i => i.Date).HasDefaultValueSql("GETDATE()");
            });
            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.HasKey(ii => ii.Id);
                entity.Property(ii => ii.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(ii => ii.VatRate).HasColumnType("decimal(18,2)");

                entity.HasOne(ii => ii.Invoice)
                      .WithMany(i => i.Items)
                      .HasForeignKey(ii => ii.InvoiceId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ii => ii.Product)
                      .WithMany(p => p.InvoiceEntries)
                      .HasForeignKey(ii => ii.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.Property(w => w.Name).IsRequired();

                entity.HasMany(w => w.Invoices)
                      .WithOne(i => i.Warehouse)
                      .HasForeignKey(i => i.WarehouseId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Counterparty>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired();

                entity.HasMany(c => c.Invoices)
                      .WithOne(i => i.Counterparty)
                      .HasForeignKey(i => i.CounterpartyId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
