using System.ComponentModel.DataAnnotations.Schema;

namespace ims.data.Entities
{
    public partial class UserRole
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public long Userid { get; set; }
        [ForeignKey("Role")]
        public long Roleid { get; set; }
        public long? Aid { get; set; }

        public UserAction A { get; set; }
        public Role Role { get; set; }
        public User User { get; set; }
    }
}
