using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ims.data.Entities
{
    public class ShoeModel
    {
        public int Id { get; set; }


        public string ModelName { get; set; }

        [ForeignKey("MachineCode")]
        public int MachineCodeId { get; set; }
        public virtual MachineCode MachineCode { get; set; }

        [ForeignKey("Sole")]
        public int SoleId { get; set; }
        public virtual Sole Sole { get; set; }

        public string ImageUrl { get; set; }
        public string ImageThumbnail { get; set; }

        public ICollection<Shoe> Shoes { get; set; }

        [ForeignKey("Document")]
        public int ImageId { get; set; }
    }


    public class Shoe
    {
        public int ShoeId { get; set; }


        [ForeignKey("ShoeModel")]
        public int ShoeModelId { get; set; }
        public virtual ShoeModel ShoeModel { get; set; }



        //public int SizeId { get; set; }
        //public virtual Size Size { get; set; }

        [ForeignKey("Color")]
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }

        [ForeignKey("Size")]
        [Required]
        public int SizeId { get; set; }
        public virtual Size Size { get; set; }


        public virtual DateTime EntryDate { get; set; } = DateTime.Now;

        public string ImageUrl { get; set; }
        public string ImageThumbnail { get; set; }

        [ForeignKey("Sex")]
        public int SexId { get; set; }
        public virtual Sex Sex { get; set; }




        public string ModelUnique { get; set; }


        public string DisplayName()
        {
            return $"{this.ShoeModel.ModelName}-{this.ShoeModel.MachineCode.MachineCodeName}-{this.Size}-{this.Color.ColorName}-{this.GetGender()}";
        }


        public string GetGender()
        {
            return SexId == 1 ? "Male" : "Female";
        }


        //public string DisplayName { get {
        //        return $"{this.ShoeModel.ModelName}-{this.ShoeModel.MachineCode.MachineCodeName}-{this.Size}-{this.Color.ColorName}";
        //    } }


    }
}
