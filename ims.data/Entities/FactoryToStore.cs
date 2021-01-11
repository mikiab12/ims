using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ims.data.Entities
{
    public class Transaction_Commons
    {

        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        [ForeignKey("User")]
        public long InitiatorId { get; set; }
        public User Initiator { get; set; }

        [ForeignKey("User")]
        public long ConfirmedById { get; set; }
        public User ConfirmedBy { get; set; }

        [ForeignKey("Workflow")]
        public Guid WorkflowId { get; set; }

        public int TotalNumber { get; set; }

        public string Description { get; set; }
        public List<ShoeTransactionList> ShoeList { get; set; }
    }

    public class FactoryToStore : Transaction_Commons
    {
        
        [ForeignKey("Factory")]
        public int FactoryId { get; set; }
        public virtual Factory Factory { get; set; }

        public string fptv { get; set; }
        [ForeignKey("Document")]
        public int FPTV_Id { get; set; }

        public string fprr { get; set; }

        [ForeignKey("Document")]
        public int FPRR_ID { get; set; }

    }

    public class StoreToShop : Transaction_Commons
    {
        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }

        public string dn { get; set; }

        [ForeignKey("Document")]
        public int DN_Id { get; set; }
    }

    public class PurchaseToStore : Transaction_Commons
    {
        [ForeignKey("Supplier")]
        public int SuppilerId { get; set; }
        public Supplier Supplier { get; set; }

        public string InvoiceNumber { get; set; }
        public double TotalPrice { get; set; }

        public string grn { get; set; }

        [ForeignKey("Document")]
        public int GRN_Id { get; set; }

    }

    public class ShopToStore : Transaction_Commons
    {
        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }

        public string mrn { get; set; }

        [ForeignKey("Document")]
        public int MRN_Id { get; set; }
    }

    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TIN { get; set; }
        public string Address { get; set; }
    }

    public enum TransactionTypes
    {
        FactoryToStore = 1,
        StoreToShop = 2,
        PurchaseToStore = 3,
        ShopToStore = 4,
        SalesReport = 5,
    }

    public class ShoeTransactionList
    {
        public int ID { get; set; }

        [ForeignKey("Shoe")]
        public int ShoeId { get; set; }
        public Shoe Shoe { get; set; }

        public int Quantity { get; set; }

        public double? UnitPrice { get; set; }

        public TransactionTypes TransactionType { get; set; }

        public int TransactionId { get; set; }
    }

    public class SalesReport
    {
        public int Id { get; set; }

        [ForeignKey("Shop")]
        public int ShopId { get; set; }


        public string reference { get; set; }

        [ForeignKey("Document")]
        public int referenceId { get; set; }

        [ForeignKey("User")]
        public long reportedBy { get; set; }

        public int totalNumber { get; set; }

        public string description { get; set; }

        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }

        [NotMapped]
        public List<ShoeTransactionList> ShoeList { get; set; }
    }
}
