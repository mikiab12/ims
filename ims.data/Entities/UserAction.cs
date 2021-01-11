using System.Collections.Generic;

namespace ims.data.Entities
{
    public partial class UserAction
    {
        public UserAction()
        {
            Userrole = new HashSet<UserRole>();
        }

        public long Id { get; set; }
        public long? Timestamp { get; set; }
        public string Username { get; set; }
        public int Actiontypeid { get; set; }
        public string Remark { get; set; }
        public long? Usernamenavigationid { get; set; }

        public ActionType Actiontype { get; set; }
        public User Usernamenavigation { get; set; }
        public ICollection<UserRole> Userrole { get; set; }
    }
}
