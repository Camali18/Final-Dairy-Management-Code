using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DAL
    {
        public LoginData Login(string username, string password)
        {
            LoginData uinfo = new LoginData();
            DatabaseClass ctx = new DatabaseClass();
            var res = ctx.Logins.Where(e => e.UserName == username && e.Password == password).FirstOrDefault();
            uinfo.username = res.UserName;
            uinfo.password = res.Password;
            uinfo.role = res.Role;
            uinfo.uid = res.Uid;
            return uinfo;
        }
        public MilkDailyData MilkData(int userid, int quantity, int fatpercid, string time, DateTime date)
        {
            MilkDailyData minfo = new MilkDailyData();
            DatabaseClass ctx = new DatabaseClass();
            Product p = new Product();
            p.VendorUid = userid;
            p.MilkQuantity = quantity;
            p.Time = time;
            p.Date = date;
            p.FatPercentId = fatpercid;
            var r = ctx.PriceMaintains.Where(e => e.PriceId == fatpercid).Select(e => e.PriceperLit).FirstOrDefault();
            p.Price = r * quantity;
            ctx.Products.Add(p);
            ctx.SaveChanges();
            return minfo;
        }
        public AddVendorData VendorDetails(string username, string password, string address)
        {
            AddVendorData vinfo = new AddVendorData();
            DatabaseClass ctx = new DatabaseClass();
            Login l = new Login();
            l.UserName = username;
            l.Password = password;
            l.Role = "Vendor";
            l.Address = address;
            ctx.Logins.Add(l);
            ctx.SaveChanges();
            return vinfo;
        }
        public AddFeedData FeedDetails(string name, int price, string description, int stock)
        {
            AddFeedData finfo = new AddFeedData();
            DatabaseClass ctx = new DatabaseClass();
            FeedItem f = new FeedItem();
            f.ItemName = name;
            f.ItemPrice = price;
            f.ItemDescription = description;
            f.ItemStock = stock;
            ctx.FeedItems.Add(f);
            ctx.SaveChanges();
            return finfo;
        }
        public AddFeedTransData TransDetails(int itemid, int qty, DateTime date, int vid)
        {
            AddFeedTransData tinfo = new AddFeedTransData();
            DatabaseClass ctx = new DatabaseClass();
            FeedTransaction t = new FeedTransaction();
            t.ItemName = ctx.FeedItems.Where(l => l.FeedItemId == itemid).Select(l => l.ItemName).FirstOrDefault();
            t.ItemQuantity = qty;
            t.FeedTransDate = date;
            t.ItemId = itemid;
            var r = ctx.FeedItems.Where(e => e.FeedItemId == itemid).Select(e => e.ItemPrice).FirstOrDefault();
            t.ItemAmount = r * qty;
            //t.ItemAmount = amount;
            t.VendorId = vid;
            ctx.FeedTransactions.Add(t);
            ctx.SaveChanges();
            return tinfo;
        }
        public SupplierPayData PaymentDetails(int month, int year, int vid)
        {
            SupplierPayData sinfo = new SupplierPayData();
            DatabaseClass ctx = new DatabaseClass();
            FeedSupplierPayment f = new FeedSupplierPayment();
            f.Year = year;
            f.Month = month;
            f.TotalAmount = ctx.FeedTransactions.Where(d => (int?)d.FeedTransDate.Month == month && (int?)d.VendorId == vid).Select(d => d.ItemAmount).Sum();
            f.VendorId = vid;
            ctx.FeedSupplierPayments.Add(f);
            ctx.SaveChanges();
            return sinfo;
        }
        public ManagerPaytoVendData VendorPayDetails(int month, int year, int vid)
        {
            ManagerPaytoVendData dinfo = new ManagerPaytoVendData();
            DatabaseClass ctx = new DatabaseClass();
            VendorPayment v = new VendorPayment();
            v.Year = year;
            v.Month = month;
            double ma = ctx.Products.Where(e => (int?)e.Date.Month == month && (int?)e.VendorUid == vid).Select(e => e.Price).Sum();
            v.MilkAmount = ma;
            double fa = ctx.FeedTransactions.Where(d => (int?)d.FeedTransDate.Month == month && (int?)d.VendorId == vid).Select(d => d.ItemAmount).Sum();
            v.FeedAmount = fa;
            v.GrossAmount = ma - fa;
            v.VendorId = vid;
            ctx.VenderPayments.Add(v);
            ctx.SaveChanges();
            return dinfo;
        }

    }
}
