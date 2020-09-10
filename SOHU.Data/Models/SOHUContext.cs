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
        public virtual DbSet<Membership> Membership { get; set; }
        public virtual DbSet<MembershipPayment> MembershipPayment { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductConfig> ProductConfig { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartDetail> CartDetail { get; set; }
        public virtual DbSet<CartDetail> Menu { get; set; }
        public virtual DbSet<CartDetail> Role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SOHU.Data.Helpers.AppGlobal.ConectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Config>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code).HasMaxLength(4000);

                entity.Property(e => e.CodeName).HasMaxLength(4000);

                entity.Property(e => e.CodenameSub).HasMaxLength(4000);

                entity.Property(e => e.GroupName)
                    .HasColumnName("GroupName")
                    .HasMaxLength(4000);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BuyId).HasColumnName("BuyID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Gbpexchange)
                    .HasColumnName("GBPExchange")
                    .HasColumnType("money");

                entity.Property(e => e.InvoiceCode).HasMaxLength(4000);

                entity.Property(e => e.InvoiceCreated).HasColumnType("datetime");

                entity.Property(e => e.MakeCode).HasMaxLength(4000);

                entity.Property(e => e.MakeSideId).HasColumnName("MakeSideID");

                entity.Property(e => e.ManageCode).HasMaxLength(4000);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.SellId).HasColumnName("SellID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Tax).HasColumnType("money");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.Property(e => e.TotalDebt).HasColumnType("money");

                entity.Property(e => e.TotalDiscount).HasColumnType("money");

                entity.Property(e => e.TotalNoTax).HasColumnType("money");

                entity.Property(e => e.TotalPaid).HasColumnType("money");

                entity.Property(e => e.TotalShipCost).HasColumnType("money");

                entity.Property(e => e.TotalTax).HasColumnType("money");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Gbpexchange)
                    .HasColumnName("GBPExchange")
                    .HasColumnType("money");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.ManageCode).HasMaxLength(4000);

                entity.Property(e => e.ManufacturingCode).HasMaxLength(4000);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.ProductCode).HasMaxLength(4000);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("money");

                entity.Property(e => e.Tax).HasColumnType("money");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.Property(e => e.TotalDiscount).HasColumnType("money");

                entity.Property(e => e.TotalNoTax).HasColumnType("money");

                entity.Property(e => e.TotalTax).HasColumnType("money");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<InvoicePayment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Gbpexchange)
                    .HasColumnName("GBPExchange")
                    .HasColumnType("money");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.PaymentCreated).HasColumnType("datetime");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.TotalPayment).HasColumnType("money");
            });

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Account).HasMaxLength(4000);

                entity.Property(e => e.Address).HasMaxLength(4000);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CitizenIdentification).HasMaxLength(4000);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(4000);

                entity.Property(e => e.FirstName).HasMaxLength(4000);

                entity.Property(e => e.FullName).HasMaxLength(4000);

                entity.Property(e => e.Guicode)
                    .HasColumnName("GUICode")
                    .HasMaxLength(4000);

                entity.Property(e => e.LastName).HasMaxLength(4000);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Passport).HasMaxLength(4000);

                entity.Property(e => e.Password).HasMaxLength(4000);

                entity.Property(e => e.Phone).HasMaxLength(4000);

                entity.Property(e => e.Points).HasColumnType("money");
            });

            modelBuilder.Entity<MembershipPayment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountNumber).HasMaxLength(4000);

                entity.Property(e => e.BankId).HasColumnName("BankID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.MembershipId).HasColumnName("MembershipID");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Author).HasMaxLength(4000);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ContentMain).HasMaxLength(4000);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Image).HasMaxLength(4000);

                entity.Property(e => e.ImageThumbnail).HasMaxLength(4000);

                entity.Property(e => e.MetaDescription).HasMaxLength(4000);

                entity.Property(e => e.MetaKeyword).HasMaxLength(4000);

                entity.Property(e => e.MetaTitle).HasMaxLength(4000);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Tags).HasMaxLength(4000);

                entity.Property(e => e.Title).HasMaxLength(4000);

                entity.Property(e => e.Urlcode)
                    .HasColumnName("URLCode")
                    .HasMaxLength(4000);
            });

            modelBuilder.Entity<ProductConfig>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConfigId).HasColumnName("ConfigID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(4000);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Title).HasMaxLength(4000);

                entity.Property(e => e.Value).HasColumnType("money");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                
                entity.Property(e => e.Tax).HasColumnType("money");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.Property(e => e.TotalDebt).HasColumnType("money");

                entity.Property(e => e.TotalDiscount).HasColumnType("money");

                entity.Property(e => e.TotalNoTax).HasColumnType("money");

                entity.Property(e => e.TotalPaid).HasColumnType("money");

                entity.Property(e => e.TotalShipCost).HasColumnType("money");

                entity.Property(e => e.TotalTax).HasColumnType("money");

                entity.Property(e => e.Gbpexchange)
                   .HasColumnName("GBPExchange")
                   .HasColumnType("money");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<CartDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID").UseIdentityColumn();

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Tax).HasColumnType("money");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.Property(e => e.TotalDiscount).HasColumnType("money");

                entity.Property(e => e.TotalNoTax).HasColumnType("money");

                entity.Property(e => e.TotalTax).HasColumnType("money");

                entity.Property(e => e.Gbpexchange)
                   .HasColumnName("GBPExchange")
                   .HasColumnType("money");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
