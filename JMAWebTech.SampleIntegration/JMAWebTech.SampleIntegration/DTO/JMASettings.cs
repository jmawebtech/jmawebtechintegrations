using System.ComponentModel;
using System;
using System.Collections.Generic;

namespace JMA.Plugin.Accounting.QuickBooks.DTO
{
    public class JMASettings
    {
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public string AmazonMerchantID { get; set; }
        public string AmazonMarketPlaceID { get; set; }
        public string ARAAccount { get; set; }
        public string BillAPAAccount { get; set; }
        public string ClassRef { get; set; }
        public string CompanyID { get; set; }
        public string CompletedOrderStatus { get; set; }
        public string ConnectionString { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerKeySecret { get; set; }
        public string CustomerID { get; set; }
        public string DataMode { get; set; }
        public string DebugMode { get; set; }
        public string DefaultAccount { get; set; }
        public string DefaultAccountID { get; set; }
        public string DefaultSalesRep { get; set; }
        public bool DeleteQBDuplicates { get; set; }
        public string DNNUserName { get; set; }
        public string DNNPassword { get; set; }
        public int DNNPortalID { get; set; }
        public string DNNUrl { get; set; }
        public string EBayUserToken { get; set; }
        public DateTime EBayUserTokenDate { get; set; }
        public string ECommerceSystem { get; set; }
        public int HighestOrder { get; set; }
        public bool InsertZeroOrders { get; set; }
        public string InvDiscAcct { get; set; }
        public string InvoiceMode { get; set; }
        public string ItemAssetAcct { get; set; }
        public string ItemCOGSAcct { get; set; }
        public string ItemIncomeAcct { get; set; }
        public DateTime LastDownloadUtc { get; set; }
        public string Licensekey { get; set; }
        public int LowestOrder { get; set; }
        public bool MarkOrderComplete { get; set; }
        public decimal MerchantFee { get; set; }
        public decimal MerchantPercent { get; set; }
        public string MerchantVendorAcct { get; set; }
        public bool ModifyRecords { get; set; }
        public int OrderLimit { get; set; }
        public string OrderPrefix { get; set; }
        public string OrderStatus { get; set; }
        public string QBDAccessToken { get; set; }
        public string QBDAccessTokenSecret { get; set; }
        public string QBDCompanyID { get; set; }
        public List<string> QuickBooksCustomFields { get; set; }
        public DateTime QuickBooksTrialStartDate { get; set; }
        public string QuickBooksOnlineEmail { get; set; }
        public string PmtARAccount { get; set; }
        public bool POSSyncImages { get; set; }
        public int POSNumberProducts { get; set; }
        public string SalesTaxVendor { get; set; }
        public bool SetExported { get; set; }
        public string StandardTerms { get; set; }
        public int StartOrderNumber { get; set; }
        public string StringOrders { get; set; }
        public string TestingResult { get; set; }
        public bool ToBePrinted { get; set; }
        public bool UpdateInStockFromQB { get; set; }
        public bool UseQBD { get; set; }
        public bool UseUK { get; set; }
        public bool VoidOnly { get; set; }
    }
}