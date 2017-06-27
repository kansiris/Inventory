namespace Inventory.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Franchise> Franchises { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<Payment_Method_Types> Payment_Method_Types { get; set; }
        public virtual DbSet<Purchase_Order> Purchase_Order { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Staff_status> Staff_status { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Franchise>()
                .Property(e => e.Franchise_Id)
                .IsFixedLength();

            modelBuilder.Entity<Franchise>()
                .Property(e => e.Staff_Id)
                .IsFixedLength();

            //modelBuilder.Entity<Invoice>()
            //    .Property(e => e.Invoice_Number)
            //    .IsFixedLength();

            //modelBuilder.Entity<Invoice>()
            //    .Property(e => e.Amount)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<Invoice>()
            //    .Property(e => e.Staff_Id)
            //    .IsFixedLength();

            //modelBuilder.Entity<Invoice>()
            //    .Property(e => e.Vendor_Id)
            //    .IsFixedLength();

            modelBuilder.Entity<Item>()
                .Property(e => e.SKU)
                .IsFixedLength();

            modelBuilder.Entity<Item>()
                .Property(e => e.Quantity)
                .IsFixedLength();

            modelBuilder.Entity<Item>()
                .Property(e => e.UnitOfMeasure_Id)
                .IsFixedLength();

            modelBuilder.Entity<Item>()
                .Property(e => e.Cost_Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Item>()
                .Property(e => e.Selling_Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Payments>()
                .Property(e => e.Payment_Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Payment_Method_Types>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<Payment_Method_Types>()
                .HasMany(e => e.Payments)
                .WithOptional(e => e.Payment_Method_Types)
                .HasForeignKey(e => e.Payment_Method);

            modelBuilder.Entity<Purchase_Order>()
                .Property(e => e.PO_No)
                .IsFixedLength();

            modelBuilder.Entity<Purchase_Order>()
                .Property(e => e.Staff_ID)
                .IsFixedLength();

            modelBuilder.Entity<Purchase_Order>()
                .Property(e => e.Vendor_ID)
                .IsFixedLength();

            modelBuilder.Entity<Purchase_Order>()
                .Property(e => e.NetAmt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Purchase_Order>()
                .Property(e => e.TaxAmt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Purchase_Order>()
                .Property(e => e.GrossAmt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Staff_Id)
                .IsFixedLength();

            modelBuilder.Entity<Staff_status>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<UnitOfMeasure>()
                .Property(e => e.UnitOfMeasure_Id)
                .IsFixedLength();

            modelBuilder.Entity<UnitOfMeasure>()
                .Property(e => e.UnitOfMeasure_Name)
                .IsFixedLength();

            modelBuilder.Entity<Vendor>()
                .Property(e => e.Vendor_Id)
                .IsFixedLength();
        }
    }
}
