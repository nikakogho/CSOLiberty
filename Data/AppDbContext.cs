using CSOLiberty.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSOLiberty.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Seller> Sellers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var orders = modelBuilder.Entity<Order>();

            orders.HasOne(o => o.Parent).WithMany(o => o.Children).HasForeignKey(o => o.ParentID).IsRequired(false); //.OnDelete(DeleteBehavior.NoAction);
            orders.HasOne(o => o.Client).WithMany(c => c.Orders).HasForeignKey(o => o.ClientID).IsRequired();
            orders.HasOne(o => o.Seller).WithMany(s => s.Orders).HasForeignKey(o => o.SellerID).IsRequired();

            modelBuilder.Entity<Seller>().HasOne(s => s.Boss).WithMany(s => s.Employees).HasForeignKey(s => s.BossID).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
