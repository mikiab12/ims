using ims.data.Entities;
using System.Collections.Generic;

namespace ims.domain.Transaction.Models
{
    public class ShopToStoreModel : BaseModel
    {
        public int shopId { get; set; }
        public int storeId { get; set; }
        public string mrn { get; set; }
        public Document mrndocument { get; set; }
        public int totalNumber { get; set; }
        public List<ShoeTransactionList> shoeList { get; set; }


    }
}
