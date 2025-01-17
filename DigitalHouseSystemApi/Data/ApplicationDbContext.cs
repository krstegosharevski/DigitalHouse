﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DigitalHouseSystemApi.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace DigitalHouseSystemApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int, 
            IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>,
            IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
             
            builder.Entity<Photo>()
                .HasOne(ur => ur.Product)
                .WithOne(u => u.Photo)
                .HasForeignKey<Photo>(ur => ur.ProductId)
                .IsRequired(false);

            builder.Entity<Photo>()
                .HasOne(ur => ur.Category)
                .WithOne(u => u.Photo)
                .HasForeignKey<Photo>(ur => ur.CategoryId)
                .IsRequired(false);

            builder.Entity<Product>()
                .HasOne(ur => ur.Category)
                .WithMany(u => u.Products)
                .HasForeignKey(ur => ur.CategoryId)
                .IsRequired();

            builder.Entity<Product>()
                .HasOne(ur => ur.Brand)
                .WithMany(u => u.Products)
                .HasForeignKey(ur => ur.BrandId)
                .IsRequired();

            builder.Entity<ProductColor>()
                .HasKey(ur => new {ur.ProductId, ur.ColorId});

            builder.Entity<ProductColor>()
                .HasOne(ur => ur.Product)
                .WithMany(u => u.ProductColors)
                .HasForeignKey(ur => ur.ProductId);

            builder.Entity<ProductColor>()
                .HasOne(ur => ur.Color)
                .WithMany(u => u.ProductColors)
                .HasForeignKey(ur => ur.ColorId);
        }

    }
}
