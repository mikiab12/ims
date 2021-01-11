using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ims.data.Entities
{
    public class Shop
    {
        public int ShopId { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public string ShopName { get; set; }
        public string ShopLocation { get; set; }

        public ICollection<Shop> Shops { get; set; }


    }
}
