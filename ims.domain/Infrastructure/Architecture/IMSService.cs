using ims.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ims.domain.Infrastructure.Architecture
{
    public interface iIMSService
    {
        StoreDbContext GetContext();
        void SetContext(StoreDbContext value);
    }

    public class IMSService : iIMSService
    {
        protected StoreDbContext Context;

        public virtual StoreDbContext GetContext()
        {
            return Context;
        }

        public virtual void SetContext(StoreDbContext value)
        {
            Context = value;
        }
    }
}
