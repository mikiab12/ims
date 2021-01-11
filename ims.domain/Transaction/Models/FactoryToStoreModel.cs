using ims.data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ims.domain.Transaction.Models
{
    public class BaseModel
    {
        public string description { get; set; }
        public DateTime date { get; set; }
        public Guid wfid { get; set; }

    }

    public class TransactionRequestModel
    {
        public string description { get; set; }
        public Guid wfid { get; set; }
    }

    public class FactoryToStoreModel : BaseModel
    {
        public int factoryId { get; set; }
        public string fptv { get; set; }
        public string fprr { get; set; }
        public int storeId { get; set; }
        public int totalNumber { get; set; }
        public List<ShoeTransactionList> shoeList { get; set; }
        public Document fptvdocument { get; set; }
        public Document fprrdocument { get; set; }
        //public IFormFile FPRR_Document { get; set; }

    }

    public class SalesReportModel
    {
        public int shopId { get; set; }
        public string reference { get; set; }
        public Document referenceDoc { get; set; }
        public int reportedBy { get; set; }
        public int totalNumber { get; set; }
        public string description { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public List<ShoeTransactionList> shoeList { get; set; }

    }
}
