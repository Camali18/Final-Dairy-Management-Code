using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace DataAccessLayer
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DatabaseClass : DbContext
    {
        public DatabaseClass()
            : base("name=DatabaseClass")
        {
        }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<PriceMaintain> PriceMaintains { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<FeedItem> FeedItems { get; set; }
        public virtual DbSet<FeedTransaction> FeedTransactions { get; set; }
        public virtual DbSet<VendorPayment> VenderPayments { get; set; }
        public virtual DbSet<FeedSupplierPayment> FeedSupplierPayments { get; set; }
    }
    public class Login
    {
        [Key]
        public int Uid { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Address { get; set; }
    }
    public class PriceMaintain
    {
        [Key]
        public int PriceId { get; set; }
        [Required]
        public string FatPercent { get; set; }
        [Required]
        public int PriceperLit { get; set; }

    }
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public double MilkQuantity { get; set; }
        [Required]
        public int FatPercentId { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int VendorUid { get; set; }
        [Required]
        public int Price { get; set; }


        [ForeignKey("VendorUid")]
        public Login ForeignVendor1 { get; set; }

        [ForeignKey("FatPercentId")]
        public PriceMaintain ForeignFatPercId { get; set; }
        
    }
    public class FeedItem
    {
        [Key]
        public int FeedItemId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public int ItemPrice { get; set; }
        [Required]
        public string ItemDescription { get; set; }
        [Required]
        public int ItemStock { get; set; }
    }
    public class FeedTransaction
    {
        [Key]
        public int FeedTransId { get; set; }
        [Required]
        public DateTime FeedTransDate { get; set; }

        public int ItemId { get; set; }
        [Required]
        public String ItemName { get; set; }
        [Required]
        public int ItemQuantity { get; set; }
        [Required]
        public int ItemAmount { get; set; }
        [Required]
        public int VendorId { get; set; }

        [ForeignKey("VendorId")]
        public Login ForeignVendor2 { get; set; }
    }
    public class VendorPayment
    {
        [Key]
        public int VendorPaymentId { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public double MilkAmount { get; set; }
        [Required]
        public double FeedAmount { get; set; }
        [Required]
        public double GrossAmount { get; set; }
        [Required]
        public int VendorId { get; set; }

        [ForeignKey("VendorId")]
        public Login ForeignVendor3 { get; set; }
    }
    public class FeedSupplierPayment
    {
        [Key]
        public int FeedSuppPaymentId { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public double TotalAmount { get; set; }
        [Required]
        public int VendorId { get; set; }

        [ForeignKey("VendorId")]
        public Login ForeignVendor4 { get; set; }
    }
}



