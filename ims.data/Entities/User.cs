using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ims.data.Entities
{
    public class User
    {
        public User()
        {
            Useraction = new HashSet<UserAction>();
            Userrole = new HashSet<UserRole>();
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [Display(Name ="Username")]
        public string Username { get; set; }
        public string Email { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string Fullname { get; set; }
        public int Status { get; set; }
        public long Regon { get; set; }
        public int? Employeeid { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        [Display(Name = "Phone")]
        public string PhoneNo { get; set; }
        public ICollection<UserAction> Useraction { get; set; }
        public ICollection<UserRole> Userrole { get; set; }

    }
}
