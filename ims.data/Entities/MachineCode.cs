using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ims.data.Entities
{
    public class MachineCode
    {

        public int MachineCodeId { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public string MachineCodeName { get; set; }
        public decimal Price { get; set; }

        public ICollection<Shoe> Shoes { get; set; }

    }
}
