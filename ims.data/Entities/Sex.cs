using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ims.data.Entities
{
    public class Sex
    {
        public int Id { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public string SexName { get; set; }

        public ICollection<Shoe> Shoes { get; set; }

    }
}
