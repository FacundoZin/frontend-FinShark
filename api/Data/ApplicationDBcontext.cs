using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBcontext : IdentityDbContext<AppUser>
    {
        public ApplicationDBcontext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Portfolio> portfolios { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(x => x.HasKey(p => new { p.appuserID, p.stockid }));

            builder.Entity<Portfolio>()
             .HasOne(u => u.appUser)
             .WithMany(u => u.portfolios)
             .HasForeignKey(p => p.appuserID);

            builder.Entity<Portfolio>()
             .HasOne(u => u.stock)
             .WithMany(u => u.portfolios)
             .HasForeignKey(p => p.stockid);


            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}