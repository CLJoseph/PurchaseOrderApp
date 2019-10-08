﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191007181001_initial-009")]
    partial class initial009
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("devDb")
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccess.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("DataAccess.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("Organisation");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PersonName");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DataAccess.Entities.TblLookup", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Lookup")
                        .HasMaxLength(50);

                    b.Property<string>("Note")
                        .HasMaxLength(150);

                    b.Property<byte[]>("RowVersionNo")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Value")
                        .HasMaxLength(150);

                    b.HasKey("ID");

                    b.ToTable("Lookup");
                });

            modelBuilder.Entity("DataAccess.Entities.TblOrganisation", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<Guid>("ApplicationUserId");

                    b.Property<string>("Contact");

                    b.Property<string>("ContactEmail");

                    b.Property<string>("ContactNo");

                    b.Property<string>("Name");

                    b.Property<byte[]>("RowVersionNo")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserId", "Name")
                        .IsUnique()
                        .HasName("IX_UniqueOrg")
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Organisations");
                });

            modelBuilder.Entity("DataAccess.Entities.TblOrganisationItem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand");

                    b.Property<string>("Code")
                        .HasMaxLength(25);

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<string>("Name");

                    b.Property<string>("Price")
                        .HasMaxLength(50);

                    b.Property<byte[]>("RowVersionNo")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("TaxRate")
                        .HasMaxLength(50);

                    b.Property<Guid>("TblOrganisationId");

                    b.HasKey("ID");

                    b.HasIndex("TblOrganisationId", "Code")
                        .IsUnique()
                        .HasName("IX_UniqueItem")
                        .HasFilter("[Code] IS NOT NULL");

                    b.ToTable("OrganisationItems");
                });

            modelBuilder.Entity("DataAccess.Entities.TblPurchaseOrder", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicationUserId");

                    b.Property<string>("Budget");

                    b.Property<string>("Code")
                        .HasMaxLength(15);

                    b.Property<string>("DateFullfilled")
                        .HasMaxLength(30);

                    b.Property<string>("DateRaised")
                        .HasMaxLength(30);

                    b.Property<string>("DateRequired")
                        .HasMaxLength(30);

                    b.Property<string>("DeliverTo")
                        .HasMaxLength(255);

                    b.Property<string>("DeliverToDetail")
                        .HasMaxLength(500);

                    b.Property<string>("InvoiceTo")
                        .HasMaxLength(255);

                    b.Property<string>("InvoiceToDetail")
                        .HasMaxLength(500);

                    b.Property<string>("Note")
                        .HasMaxLength(255);

                    b.Property<string>("Price")
                        .HasMaxLength(12);

                    b.Property<byte[]>("RowVersionNo")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Status");

                    b.Property<string>("Tax")
                        .HasMaxLength(8);

                    b.Property<string>("To")
                        .HasMaxLength(255);

                    b.Property<string>("ToDetail")
                        .HasMaxLength(255);

                    b.Property<string>("ToEmail");

                    b.Property<string>("Total")
                        .HasMaxLength(15);

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserId", "Code")
                        .IsUnique()
                        .HasName("IX_UniquePO")
                        .HasFilter("[Code] IS NOT NULL");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("DataAccess.Entities.TblPurchaseOrderItem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand");

                    b.Property<string>("Code")
                        .HasMaxLength(25);

                    b.Property<string>("Description")
                        .HasMaxLength(250);

                    b.Property<string>("Name");

                    b.Property<string>("Price")
                        .HasMaxLength(15);

                    b.Property<Guid?>("PurchaseOrderID");

                    b.Property<string>("Quantity");

                    b.Property<byte[]>("RowVersionNo")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Tax")
                        .HasMaxLength(15);

                    b.Property<string>("TaxCode");

                    b.Property<string>("Total")
                        .HasMaxLength(15);

                    b.HasKey("ID");

                    b.HasIndex("PurchaseOrderID");

                    b.ToTable("PurchaseOrderItems");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DataAccess.Entities.TblOrganisation", b =>
                {
                    b.HasOne("DataAccess.ApplicationUser", "ApplicationUser")
                        .WithMany("Organisations")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Entities.TblOrganisationItem", b =>
                {
                    b.HasOne("DataAccess.Entities.TblOrganisation")
                        .WithMany("Items")
                        .HasForeignKey("TblOrganisationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Entities.TblPurchaseOrder", b =>
                {
                    b.HasOne("DataAccess.ApplicationUser", "ApplicationUser")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Entities.TblPurchaseOrderItem", b =>
                {
                    b.HasOne("DataAccess.Entities.TblPurchaseOrder", "PurchaseOrder")
                        .WithMany("Items")
                        .HasForeignKey("PurchaseOrderID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("DataAccess.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("DataAccess.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("DataAccess.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("DataAccess.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccess.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("DataAccess.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
