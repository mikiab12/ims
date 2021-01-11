using ims.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ims.domain.Infrastructure
{
    public interface ILookupService
    {
        object GetRoles();
    }

    public class LookupService : ILookupService
    {
        private readonly StoreDbContext _StoreContext;

        public LookupService(StoreDbContext context)
        {
            _StoreContext = context;
        }

        public object GetRoles()
        {
            return _StoreContext.Role.Select(role => new { id = role.Id, name = role.Name }).ToList();
        }
    }
}
