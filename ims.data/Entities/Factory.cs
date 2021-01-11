using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ims.data.Entities
{
    public class Factory
    {
        public int FactoryId { get; set; }
        [Required]
        public string FactoryName { get; set; }
        public string LocationDescription { get; set; }
    }
}
