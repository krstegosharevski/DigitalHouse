﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DigitalHouseSystemApi.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System.Reflection.Emit;

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
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Problem> Problems { get; set; }

        /* Tariffs */
        public DbSet<InternetPackage> InternetPackages { get; set; }
        public DbSet<TariffType> TariffTypes { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<Magenta1> Magenta1s { get; set; }
        public DbSet<Magenta1Tariff> Magenta1Tariffs { get; set; }





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

            builder.Entity<Photo>()
                .HasOne(ur => ur.Problem)
                .WithOne(u => u.Photo)
                .HasForeignKey<Photo>(ur => ur.ProblemId)
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

            builder.Entity<ShoppingCart>()
                .HasOne(ur => ur.AppUser)
                .WithMany(u => u.ShoppingCarts)
                .HasForeignKey(ur => ur.AppUserId);
                
            builder.Entity<ShoppingCartItem>()
                .HasOne(ur => ur.ShoppingCart)
                .WithMany(u => u.Items)
                .HasForeignKey(ur => ur.ShoppingCartId);

            builder.Entity<ShoppingCartItem>()
                .HasOne(ur => ur.Product)
                .WithMany()
                .HasForeignKey(ur => ur.ProductId);

            builder.Entity<ShoppingCart>()
                .Property(sc => sc.Status)
                .HasConversion<string>();

            /* Tariffs */

            builder.Entity<TariffType>()
                .Property(sc => sc.TariffCategory)
                .HasConversion<string>(); //ok
            
            builder.Entity<Tariff>()
                .HasOne(ur => ur.TariffType)
                .WithMany(u => u.Tariffs)
                .HasForeignKey(ur => ur.TariffTypeId); //ok

            builder.Entity<Magenta1>()
                .HasOne(ur => ur.AppUser)
                .WithOne()
                .HasForeignKey<Magenta1>(ur => ur.Id)
                .IsRequired(); //ok

            builder.Entity<Magenta1>()
                .HasOne(ur => ur.InternetPackage)
                .WithMany(u => u.Magenta1s)
                .HasForeignKey(ur => ur.InternetPackageId); //ok

            builder.Entity<Magenta1Tariff>()
               .HasKey(ur => new { ur.TariffId, ur.Magenta1Id }); //ok

            builder.Entity<Magenta1Tariff>()
                .HasOne(ur => ur.Magenta1)
                .WithMany(u => u.Magenta1Tariffs)
                .HasForeignKey(ur => ur.Magenta1Id); //ok

            builder.Entity<Magenta1Tariff>()
                .HasOne(ur => ur.Tariff)
                .WithMany(u => u.Magenta1Tariffs)
                .HasForeignKey(ur => ur.TariffId); //ok




        }

    }
}
