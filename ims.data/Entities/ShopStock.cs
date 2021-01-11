using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ims.data.Entities
{
    public class ShopStock
    {
        public int Id { get; set; }

        [ForeignKey("Shoe")]
        public int ShoeId { get; set; }
        public virtual Shoe Shoe { get; set; }

        [ForeignKey("Shop")]
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }

        public int Amount { get; set; }

        public int Stock { get; set; }

        public DateTime Date { get; set; }

        public int SeqNo { get; set; }
    }
}
