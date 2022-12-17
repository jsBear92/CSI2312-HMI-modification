using Microsoft.EntityFrameworkCore;
using HMI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMI.Data
{
    public class HMIContext : DbContext
    {
        public HMIContext(DbContextOptions<HMIContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Default initial account when db create
            modelBuilder.Entity<Account>().HasData(new Account { UserID = 1, Username = "admin12345", UserPassword = "administrator1", UserType = 0 });
            modelBuilder.Entity<Account>().HasData(new Account { UserID = 2, Username = "operator12345", UserPassword = "operator1", UserType = 1 });
        }
    }
}
