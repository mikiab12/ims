using System;
using System.Collections.Generic;
using System.Text;

namespace ims.domain.Infrastructure
{
    public class UserSession
    {
        public Dictionary<string, object> Content;
        public int? EmployeeID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long Role { get; set; }
        public long[] Roles { get; set; }
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastSeen { get; set; }
        public string payrollSessionID { get; set; }
        public string Token { get; set; }
        public int wsisUserID { get; set; }
       // public UserPermissions Permissions { get; set; }
        public int costCenterID {get; set;}

    }
}
