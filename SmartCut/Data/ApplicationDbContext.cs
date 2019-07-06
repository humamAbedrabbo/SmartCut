using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartCut.Models;

namespace SmartCut.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SettingModel> Settings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<StockItem> StockItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            builder.Entity<SettingModel>().HasData(new SettingModel { Id = 1, MaximumCuttingLengthInCm = 1, GramageRangePercent = 0 });

            builder.Entity<Category>().Property(p => p.Name).HasMaxLength(50).IsRequired();

            builder.Entity<StockItem>(e => {
                e.Ignore(p => p.LengthCM);
                e.Ignore(p => p.WidthCM);
            });
        }
    }
}
