using ims.data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ims.domain.Transaction.Models
{
    public enum TransactionType
    {
        FactoryToStore = 1,
        StoreToShop = 2,
        PurhcaseToStore = 3,
        ShopToStore = 4,
        SalesReport = 5
    }

    public class TransactionSearchRequest
    {
        public TransactionType Type { get; set; }
        public int? FactoryId { get; set; }
        public int? StoreId { get; set; }
        public int? ShopId { get; set; }
        public int? SupplierId { get; set; }
        public string fptv { get; set; }
        public string fprr { get; set; }
        public string dn { get; set; }
        public string grn { get; set; }
        public string mrn { get; set; }
        public string srn { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class TransactionSearchResponse
    {
        public List<FactoryToStore> FactoryToStore { get; set; }
        public List<StoreToShop> StoreToShop { get; set; }
        public List<PurchaseToStore> PurchaseToStore { get; set; }
        public List<ShopToStore> ShopToStore { get; set; }
        public TransactionSearchRequest Request { get; set; }
        public List<SalesReport> SalesReport { get; set; }
    }

    public enum StockSearchType
    {
        StoreStockType = 1,
        ShopStockType = 2
    }

    public class StockSearchRequest
    {
        public StockSearchType Type { get; set; }
        public int? StoreId { get; set; }
        public int? ShopId { get; set; }
        public DateTime Date { get; set; }
        public int[] Shoes { get; set; }
    }

    public class StockSearchResponse
    {
        public StockSearchRequest Request { get; set; }
        public List<StoreStock> StoreStocks { get; set; }
        public List<ShopStock> ShopStocks { get; set; }

    }
}
