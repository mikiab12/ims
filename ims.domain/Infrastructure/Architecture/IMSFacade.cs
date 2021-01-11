using ims.data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ims.domain.Infrastructure.Architecture
{
    public interface IiMSFacade
    {
        void PassContext(iIMSService service, StoreDbContext context);
        //void PassContext(IPWFService service, DbConnection connection);
        //void PassContext(IPWFService service);

        DbTransaction BeginTransaction();
        TReturn Transact<TReturn>(Func<IDbContextTransaction, TReturn> func);
        void Transact(Action<IDbContextTransaction> func);
    }

    public class IMSFacade : IiMSFacade
    {
        protected IMSFacade(StoreDbContext Context)
        {
            //Connection = new PWFConnection();
            //Connection.SetContext(Context);
            // _dbConnection = Context.Database.GetDbConnection();
            _context = Context;

        }
        protected StoreDbContext _context;
        protected DbConnection _dbConnection { get; }

        public virtual void PassContext(iIMSService service, StoreDbContext context)
        {
            service.SetContext(context);
        }

        public virtual DbTransaction BeginTransaction()
        {
            _dbConnection.Open();
            return _dbConnection.BeginTransaction();
            // return Connection.GetWriteConnection().BeginTransaction();
        }
        public delegate TReturn TRansactWithWithWSISDelegate<IDbContextTransaction, TReturn>(IDbContextTransaction t, int AID);
        public delegate void TRansactWithWithWSISDelegate<IDbContextTransaction>(IDbContextTransaction t, int AID);

        public virtual TReturn Transact<TReturn>(Func<IDbContextTransaction, TReturn> func)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var ret = func.Invoke(transaction);
                    transaction.Commit();
                    return ret;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

        }

        public virtual void Transact(Action<IDbContextTransaction> func)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    func.Invoke(transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

        }
    }
}
