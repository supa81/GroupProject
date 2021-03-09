using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PawMates.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PawMates.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Owner", NormalizedName = "OWNER" });
        }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Dog> Dogs { get; set; }
    }
}
