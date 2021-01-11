using ims.data.Entities;
using System.Collections.Generic;

namespace ims.domain.Transaction.Models
{
    public class StoreToShopModel  : BaseModel
    {
        public int storeId { get; set; }
        public int shopId { get; set; }
        public string dn { get; set; }
        public Document dndocument { get; set; }
        public List<ShoeTransactionList>  shoeList { get; set; }
    }

}
