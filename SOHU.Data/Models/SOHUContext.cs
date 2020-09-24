using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SOHU.Data.Models
{
    public partial class SOHUContext : DbContext
    {
        public SOHUContext()
        {
        }

        public SOHUContext(DbContextOptions<SOHUContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Config> Config { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetail { get; set; }
        public virtual DbSet<InvoicePayment> InvoicePayment { get; set; }
        public virtual DbSet<InvoiceProperty> InvoiceProperty { get; set; }
        public virtual DbSet<Membership> Membership { get; set; }
        public virtual DbSet<MembershipPayment> MembershipPayment { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductConfig> ProductConfig { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartDetail> CartDetail { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SOHU.Data.Helpers.AppGlobal.ConectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
