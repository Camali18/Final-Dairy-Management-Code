using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    class Model
    {

    }
    public class LoginData
    {
        public int uid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
    public class AddVendorData
    {
        public string username { get; set; }
        public string password { get; set; }
        public string address { get; set; }
    }
    public class VendorData
    {
        public int VID { get; set; }
        public string VenName { get; set; }
    }
    public class MilkDailyData
    {
        [Key]
        public int Productid { get; set; }
        public double MilkQuantity { get; set; }
        public double FatPercentage { get; set; }
        public double Amount { get; set; }
        public DateTime date { get; set; }
        public string time { get; set; }
        public int VendorId { get; set; }

    }
    public class AddFeedData
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string ItemDescription { get; set; }
        public int ItemStock { get; set; }
    }

    public class AddFeedTransData
    {
        public int FeedTransId { get; set; }
        public int VendorId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public DateTime TransDate { get; set; }
        public int ItemQuantity { get; set; }
        public int ItemAmount { get; set; }
    }
    public class SupplierPayData
    {
        public int SupplierId { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public Double Amount { get; set; }
        public int VendorId { get; set; }
    }
    public class ManagerPaytoVendData
    {
        public int PayId { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int GrossAmount { get; set; }
        public int FeedAmount { get; set; }
        public int MilkAmount { get; set; }
        public int VendorId { get; set; }
        public int UserId { get; set; }
    }

    public class VendorPaymentData
    {
        public int VendorPaymentId { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public Double TotalAmount { get; set; }
        public Double Deduction { get; set; }
        public double RemainingAmount { get; set; }
        public int VendorId { get; set; }
    }

    

}



