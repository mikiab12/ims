using ims.data.Entities;
using System.Collections.Generic;

namespace ims.domain.Transaction.Models
{
    public class PurchaseToStoreModel :  BaseModel
    {
        public int supplierId { get; set; }
        public string invoiceNo { get; set; }
        public int totalNumber { get; set; }
        public int storeId { get; set; }
        public string grn { get; set; }
        public Document grndocument { get; set; }
        public List<ShoeTransactionList> shoeList { get; set; }

    }
}
