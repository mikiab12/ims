using ims.data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ims.domain.Configuration
{
    public class BaseData
    {
        public Color[] Colors { get; set; }
        public Factory[] Factories { get; set; }
        public MachineCode[] MachineCodes { get; set; }
        public Shop[] Shops { get; set; }
        public Size[] Sizes { get; set; }
        public Sole[] Soles { get; set; }
        public Store[] Stores { get; set; }
        public Supplier[] Suppliers { get; set; }
    }
}
