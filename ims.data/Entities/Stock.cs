using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ims.data.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        

        public int ShoeId { get; set; }
        public virtual Shoe Shoe { get; set; }

        public int SizeId { get; set; }
        public virtual Size Size { get; set; }
        public int Amount { get; set; }

    }
}
