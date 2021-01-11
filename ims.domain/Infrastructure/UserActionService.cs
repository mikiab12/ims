using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ims.data.Entities;
using ims.data;
using ims.domain.Admin;

namespace ims.domain.Infrastructure
{
    public interface IUserActionService
    {
        UserAction AddUserAction(UserSession session, UserActionType actionType);
        UserAction GetUserAction(long aid);
    }

    public class UserActionService : IUserActionService
    {
        private readonly StoreDbContext _storeContext;

        public UserActionService(StoreDbContext PWFContext)
        {
            _storeContext = PWFContext;
        }

        public UserAction AddUserAction(UserSession session, UserActionType type)
        {

            var actionType = GetActionType((int)type);

            //var user = _PWFContext.User.First(u => u.Username.Equals(session.Username));

            var action = new UserAction
            {
                Timestamp = DateTime.Now.Ticks,
                Actiontype = actionType,
                Username = session.Username,
            };
            _storeContext.UserAction.Add(action);
            _storeContext.SaveChanges();

            return action;
        }

        private ActionType GetActionType(int id)
        {
            return _storeContext.ActionType.First(at => at.Id == id);
        }

        public  UserAction GetUserAction(long aid)
        {
            return _storeContext.UserAction.Where(m => m.Id == aid).FirstOrDefault();
        }
    }
}
