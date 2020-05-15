using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlazorShoppingCart.API.Models
{
    public partial class HalloweenDBContext : DbContext
    {
        //public HalloweenDBContext()
        //{
        //}

        public HalloweenDBContext(DbContextOptions<HalloweenDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<InvoiceData> InvoiceData { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<LineItems> LineItems { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<States> States { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=localhost;Database=HalloweenDB;Trusted_Connection=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(x => x.CategoryId);

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LongName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(x => x.Email);

                entity.Property(e => e.Email)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.HasOne(d => d.StateNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(x => x.State)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customers_States");
            });

            modelBuilder.Entity<InvoiceData>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.SalesTax).HasColumnType("money");
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.HasKey(x => x.InvoiceNumber);

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreditCardType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CustEmail)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.SalesTax).HasColumnType("money");

                entity.Property(e => e.ShipMethod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Shipping).HasColumnType("money");

                entity.Property(e => e.Subtotal).HasColumnType("money");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.HasOne(d => d.CustEmailNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(x => x.CustEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Customers");
            });

            modelBuilder.Entity<LineItems>(entity =>
            {
                entity.HasKey(x => new { x.InvoiceNumber, x.ProductId });

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Extension)
                    .HasColumnType("money")
                    .HasComputedColumnSql("([UnitPrice]*[Quantity])");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.InvoiceNumberNavigation)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(x => x.InvoiceNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LineItems_Invoices");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LineItems_Products");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(x => x.ProductId);

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasColumnName("CategoryID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ImageFile)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.LongDescription)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShortDescription)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Categories");
            });

            modelBuilder.Entity<States>(entity =>
            {
                entity.HasKey(x => x.StateCode);

                entity.Property(e => e.StateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
