using System.Collections.Generic;

namespace ims.data.Entities
{
    public class ActionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserAction> Useraction { get; set; }
    }
}
