namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeedItems",
                c => new
                    {
                        FeedItemId = c.Int(nullable: false, identity: true),
                        ItemName = c.String(nullable: false),
                        ItemPrice = c.Int(nullable: false),
                        ItemDescription = c.String(nullable: false),
                        ItemStock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FeedItemId);
            
            CreateTable(
                "dbo.FeedSupplierPayments",
                c => new
                    {
                        FeedSuppPaymentId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        VendorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FeedSuppPaymentId)
                .ForeignKey("dbo.Logins", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Uid = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Uid);
            
            CreateTable(
                "dbo.FeedTransactions",
                c => new
                    {
                        FeedTransId = c.Int(nullable: false, identity: true),
                        FeedTransDate = c.DateTime(nullable: false),
                        ItemId = c.Int(nullable: false),
                        ItemName = c.String(nullable: false),
                        ItemQuantity = c.Int(nullable: false),
                        ItemAmount = c.Int(nullable: false),
                        VendorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FeedTransId)
                .ForeignKey("dbo.Logins", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId);
            
            CreateTable(
                "dbo.PriceMaintains",
                c => new
                    {
                        PriceId = c.Int(nullable: false, identity: true),
                        FatPercent = c.String(nullable: false),
                        PriceperLit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PriceId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        MilkQuantity = c.Double(nullable: false),
                        FatPercentId = c.Int(nullable: false),
                        Time = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        VendorUid = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.PriceMaintains", t => t.FatPercentId, cascadeDelete: true)
                .ForeignKey("dbo.Logins", t => t.VendorUid, cascadeDelete: true)
                .Index(t => t.FatPercentId)
                .Index(t => t.VendorUid);
            
            CreateTable(
                "dbo.VendorPayments",
                c => new
                    {
                        VendorPaymentId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        MilkAmount = c.Double(nullable: false),
                        FeedAmount = c.Double(nullable: false),
                        GrossAmount = c.Double(nullable: false),
                        VendorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VendorPaymentId)
                .ForeignKey("dbo.Logins", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VendorPayments", "VendorId", "dbo.Logins");
            DropForeignKey("dbo.Products", "VendorUid", "dbo.Logins");
            DropForeignKey("dbo.Products", "FatPercentId", "dbo.PriceMaintains");
            DropForeignKey("dbo.FeedTransactions", "VendorId", "dbo.Logins");
            DropForeignKey("dbo.FeedSupplierPayments", "VendorId", "dbo.Logins");
            DropIndex("dbo.VendorPayments", new[] { "VendorId" });
            DropIndex("dbo.Products", new[] { "VendorUid" });
            DropIndex("dbo.Products", new[] { "FatPercentId" });
            DropIndex("dbo.FeedTransactions", new[] { "VendorId" });
            DropIndex("dbo.FeedSupplierPayments", new[] { "VendorId" });
            DropTable("dbo.VendorPayments");
            DropTable("dbo.Products");
            DropTable("dbo.PriceMaintains");
            DropTable("dbo.FeedTransactions");
            DropTable("dbo.Logins");
            DropTable("dbo.FeedSupplierPayments");
            DropTable("dbo.FeedItems");
        }
    }
}
