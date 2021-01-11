using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ims.data.Entities
{
    public class Size
    {
        public Size()
        {

        }
        public Size(int size)
        {
            this.SizeNumber = size;
        }

        public int SizeId { get; set; }

        public int SizeNumber { get; set; }

        public ICollection<Shoe> Shoes { get; set; }

    }
}
