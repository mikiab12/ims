using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ims.data.Entities
{
    public class Sole
    {
        public int SoleId { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string SoleName { get; set; }

        public ICollection<Shoe> Shoes { get; set; }

    }
}
