using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        // Properties 
        public DbSet<TblPurchaseOrder>  PurchaseOrders  {get; set;}
        public DbSet<TblPurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<TblOrganisation> Organisations { get; set; }
        public DbSet<TblOrganisationItem> OrganisationItems { get; set; }
        public DbSet<TblLookup> Lookups { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)   {}
        public ApplicationDbContext() { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("devDb");
            modelBuilder.Entity<ApplicationUser>().HasMany(M => M.PurchaseOrders).WithOne(x => x.ApplicationUser).IsRequired();
            modelBuilder.Entity<ApplicationUser>().HasMany(M => M.Organisations).WithOne(x => x.ApplicationUser).IsRequired();
            modelBuilder.Entity<ApplicationUser>(ConfigureApplicationUser);

            modelBuilder.Entity<TblPurchaseOrderItem>().HasOne(i => i.PurchaseOrder).WithMany(c => c.Items).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TblPurchaseOrder>(ConfigurePurchaseOrders);
            modelBuilder.Entity<TblPurchaseOrderItem>(ConfigurePurchaseOrderItems);
            modelBuilder.Entity<TblOrganisation>(ConfigureOrganisations);
            modelBuilder.Entity<TblOrganisationItem>(ConfigureOrganisationItems);
            modelBuilder.Entity<TblLookup>(ConfigureLookup);
        }
        private void ConfigureLookup(EntityTypeBuilder<TblLookup> obj)
        {
            obj.ToTable("Lookup");
            obj.Property(p => p.Lookup).HasMaxLength(50);
            obj.Property(p => p.Value).HasMaxLength(150);
            obj.Property(p => p.Note).HasMaxLength(150);
        }
        private void ConfigureOrganisationItems(EntityTypeBuilder<TblOrganisationItem> obj)
        {
            obj.ToTable("OrganisationItems");
            obj.Property(p => p.ID).IsRequired();
            obj.Property(p => p.RowVersionNo).IsRowVersion();
            obj.Property(p => p.Code).HasMaxLength(25);
            obj.Property(p => p.Description).HasMaxLength(500);
            obj.Property(p => p.Price).HasMaxLength(50);
            obj.Property(p => p.TaxRate).HasMaxLength(50);
            obj.HasIndex(I => new { I.TblOrganisationId, I.Code }).IsUnique().HasName("IX_UniqueItem").IsUnique();
        }
        private void ConfigureOrganisations(EntityTypeBuilder<TblOrganisation> obj)
        {
            obj.ToTable("Organisations");
            obj.Property(p => p.ID).IsRequired();
            obj.HasIndex(I => new { I.ApplicationUserId, I.Name }).IsUnique().HasName("IX_UniqueOrg").IsUnique();
        }
        private void ConfigurePurchaseOrderItems(EntityTypeBuilder<TblPurchaseOrderItem> obj)
        {
            obj.ToTable("PurchaseOrderItems");
            obj.Property(p => p.ID).IsRequired();
            obj.Property(p => p.RowVersionNo).IsRowVersion();
            obj.Property(p => p.Code).HasMaxLength(25);
            obj.Property(p => p.Description).HasMaxLength(250);
            obj.Property(P => P.Price).HasMaxLength(15);
            obj.Property(P => P.Tax).HasMaxLength(15);
            obj.Property(P => P.Total).HasMaxLength(15);
        }
        private void ConfigurePurchaseOrders(EntityTypeBuilder<TblPurchaseOrder> obj)
        {
            obj.ToTable("PurchaseOrders");
            obj.Property(p => p.ID).IsRequired();             
            obj.Property(p => p.Code).HasMaxLength(15);
            obj.Property(p => p.To).HasMaxLength(255);
            obj.Property(p => p.ToEmail).HasMaxLength(255);
            obj.Property(p => p.ToPerson).HasMaxLength(255);
            obj.Property(p => p.ToDetail).HasMaxLength(255);
            obj.Property(p => p.DeliverTo).HasMaxLength(255);
            obj.Property(p => p.DeliverToDetail).HasMaxLength(500);
            obj.Property(p => p.InvoiceTo).HasMaxLength(255);
            obj.Property(p => p.InvoiceToDetail).HasMaxLength(500);
            obj.Property(p => p.DateRaised).HasMaxLength(30);
            obj.Property(p => p.DateRequired).HasMaxLength(30);
            obj.Property(p => p.DateFullfilled).HasMaxLength(30);
            obj.Property(p => p.Note).HasMaxLength(255);
            obj.Property(p => p.Price).HasMaxLength(12);
            obj.Property(p => p.Tax).HasMaxLength(8);
            obj.Property(p => p.Total).HasMaxLength(15);
            obj.HasIndex(I => new {I.ApplicationUserId,I.Code}).IsUnique().HasName("IX_UniquePO").IsUnique();
        }
        private void ConfigureApplicationUser(EntityTypeBuilder<ApplicationUser> obj)   { }
    }
}
