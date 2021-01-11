using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ims.data.Entities
{

    public class Store
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }


    public class StoreStock
    {
        public int Id { get; set; }

        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        [ForeignKey("Shoe")]
        public int ShoeId { get; set; }
        public virtual Shoe Shoe { get; set; }

        public int Amount { get; set; }

        public int Stock { get; set; }

        public DateTime Date { get; set; }

        public int SeqNo { get; set; }

    }
}
