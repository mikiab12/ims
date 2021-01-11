using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ims.data.Entities
{
    public class Color
    {
        public Color()
        {

        }

        public Color(string name)
        {
            this.ColorName = name;
        }

        public int ColorId { get; set; }
        [Required]
        public string ColorName { get; set; }

        public ICollection<Shoe> Shoes { get; set; }
    }
}
