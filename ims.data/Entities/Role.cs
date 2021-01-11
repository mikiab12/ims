using System.Collections.Generic;

namespace ims.data.Entities
{
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRole> UserRole { get; set; }
    }
}
