using ims.data;
using ims.data.Entities;
using ims.domain.Infrastructure;
using ims.domain.Infrastructure.Architecture;
using ims.domain.Transaction.Models;
using ims.domain.Transaction.StateMachines;
using ims.domain.Workflows;
using System;
using System.Collections.Generic;
using System.Text;

namespace ims.domain.Transaction
{




    public interface iTransactionFacade : IiMSFacade
    {
        void SetConfiguration(IMSConfiguration Config);
        void SetSession(UserSession session);
        void RequestMethod<T, K>(T Model) where T : BaseModel where K : BaseWorkflow<T>, new();

        void ApproveMethod<T, K>(T Model, Guid wfid, Action<T,Guid> S) where T : BaseModel where K : BaseWorkflow<T>, new();
        void ApproveMethod2<T, K>(string description, Guid wfid, Action<T,Guid> S) where T : BaseModel where K : BaseWorkflow<T>, new();
        void RejectMethod<T, K>(string description, Guid wfid) where T : BaseModel where K : BaseWorkflow<T>, new();
        void CancelMethod<T, K>(string description, Guid wfid) where T : BaseModel where K : BaseWorkflow<T>, new();
        T GetData<T, K>(Guid wfid) where T : BaseModel where K : BaseWorkflow<T>, new();
        void RequestFactoryToStoreTransfer(FactoryToStoreModel Model);
        void RequestStoreToShop(StoreToShopModel Model);
        void RequestPurchaseToStore(PurchaseToStoreModel Model);
        void RequestShopToStore(ShopToStoreModel Model);
        void ApproveFactoryToStore(FactoryToStoreModel Model, Guid wfid);
        void ApproveStoreToShop(string description, Guid wfid);
        void ApprovePurchaseToStore(string description, Guid wfid);
        void ApproveShopToStore(string description,  Guid wfid);
        void RejectFactoryToStore(string description , Guid wfid);
        void RejectStoreToShop(string description, Guid wfid);
        void RejectPurchaseToStore(string description, Guid wfid);
        void RejectShopToStore(string description, Guid wfid);
        void CancelFactoryToStore(string description, Guid wfid);
        void CancelStoreToShop(string description, Guid wfid);
        void CancelPurchaseToStore(string description, Guid wfid);
        void CancelShopToStore(string description, Guid wfid);
        void ResubmitFactoryToStore(FactoryToStoreModel Model, Guid wfid);
        void ResubmitStoreToShop(StoreToShopModel model, Guid wfid);
        void ResubmitShopToStore(ShopToStoreModel Model, Guid wfid);
        void ResubmitPurchaseToStore(PurchaseToStoreModel Model, Guid wfid);
        void AddSalesReportTransaction(SalesReportModel Model);
        FactoryToStore GetFactoryToStore(int id);
        StoreToShop GetStoreToShop(int id);
        PurchaseToStore GetPurchaseToStore(int id);
        ShopToStore GetShopToStore(int id);
        SalesReport GetSalesReport(int id);
    }

    public class TransactionFacade : IMSFacade, iTransactionFacade
    {
        public ITransactionService _transactionService;
        public IWorkflowService _wfService;
        private StoreDbContext _context;
        private UserSession _session;
        private IMSConfiguration _config;

        public TransactionFacade(StoreDbContext Context, ITransactionService transactionService,
            IWorkflowService wfService) : base(Context)
        {
            _context = Context;
            _transactionService = transactionService;
            _wfService = wfService;
            PassContext(_transactionService, Context);
            PassContext(_wfService, Context);
        }

        public void SetSession(UserSession session)
        {
            _session = session;
            _transactionService.SetSession(session);
            _wfService.SetSession(session);

        }

        public void SetConfiguration(IMSConfiguration config)
        {
            _config = config;
            _transactionService.SetConfiguration(config);
        }

        public void RequestMethod<T, K>(T Model) where T : BaseModel where K : BaseWorkflow<T> , new()
        {
            Transact(t =>
            {
                K s = new K();
                s.SetSession(_session);
                PassContext(s, _context);
                s.ConfigureMachine(Model.description, null);
                var wfid = s.Workflow.Id;
                Model.date = DateTime.Now;
                s.Fire(wfid, BaseWorkflow<T>.ParameterizedTriggers.Request, Model, Model.description, null);
            });
        }

        public void ResubmitRequest<T,K>(T Model, Guid wfid) where T : BaseModel where K : BaseWorkflow<T>, new()
        {
            Transact(t =>
            {
                K s = new K();
                s.SetSession(_session);
                PassContext(s, _context);
                s.ConfigureMachine(wfid);
                s.Fire(wfid, BaseWorkflow<T>.ParameterizedTriggers.Request, Model, Model.description, null);
            });
        }

        public T GetData<T,K>(Guid wfid) where T : BaseModel where K :BaseWorkflow<T>, new()
        {
            K wf = new K();
            wf.SetSession(_session);
            PassContext(wf, _context);
            wf.ConfigureMachine(wfid);
            return wf.GetData();
        }

        public void ApproveMethod<T, K>(T Model,Guid wfid, Action<T,Guid> S) where T : BaseModel where K : BaseWorkflow<T>, new()
        {
            Transact(t =>
            {
                K wf = new K();
                wf.SetSession(_session);
                PassContext(wf, _context);
                wf.ConfigureMachine(wfid);
                wf.Fire(wf.Workflow.Id, BaseWorkflow<T>.ParameterizedTriggers.Approve, Model, Model.description, null);
                S(Model,wfid);
            });
        }

        public void ApproveMethod2<T, K>(string description, Guid wfid, Action<T,Guid> S) where T : BaseModel where K : BaseWorkflow<T>, new()
        {
            Transact(t =>
            {
                K wf = new K();
                wf.SetSession(_session);
                PassContext(wf, _context);
                wf.ConfigureMachine(wfid);
                T Model = wf.GetData();
                wf.Fire(wf.Workflow.Id, BaseWorkflow<T>.ParameterizedTriggers.Approve, Model, description, null);
                S(Model,wfid);
            });
        }

        public void RejectMethod<T,K>(string description, Guid wfid) where T : BaseModel where K : BaseWorkflow<T>, new()
        {
            Transact(t =>
            {
                K wf = new K();
                wf.SetSession(_session);
                PassContext(wf, _context);
                wf.ConfigureMachine(wfid);
                T Model = wf.GetData();
                wf.Fire(wf.Workflow.Id, BaseWorkflow<T>.ParameterizedTriggers.Reject, description, null);
            });
        }

        public void CancelMethod<T, K>(string description, Guid wfid) where T : BaseModel where K : BaseWorkflow<T>, new()
        {
            Transact(t =>
            {
                K wf = new K();
                wf.SetSession(_session);
                PassContext(wf, _context);
                wf.ConfigureMachine(wfid);
                T Model = wf.GetData();
                wf.Fire(wf.Workflow.Id, BaseWorkflow<T>.ParameterizedTriggers.Cancel, description, null);
            });
        }



        public void RequestFactoryToStoreTransfer(FactoryToStoreModel Model)
        {
            RequestMethod<FactoryToStoreModel, FactoryToStoreWorkflow>(Model);
        }

        public void RequestStoreToShop(StoreToShopModel Model)
        {
            RequestMethod<StoreToShopModel, StoreToShopWorkflow>(Model);
        }

        public void RequestPurchaseToStore(PurchaseToStoreModel Model)
        {
            RequestMethod<PurchaseToStoreModel, PurchaseToStoreWorkflow>(Model);
        }

        public void RequestShopToStore(ShopToStoreModel Model)
        {
            RequestMethod<ShopToStoreModel, ShopToStoreWorkflow>(Model);
        }

        public void ResubmitFactoryToStore(FactoryToStoreModel Model, Guid wfid)
        {
            ResubmitRequest<FactoryToStoreModel, FactoryToStoreWorkflow>(Model, wfid);
        }

        public void ResubmitStoreToShop(StoreToShopModel model, Guid wfid)
        {
            ResubmitRequest<StoreToShopModel, StoreToShopWorkflow>(model, wfid);
        }

        public void ResubmitShopToStore(ShopToStoreModel Model, Guid wfid)
        {
            ResubmitRequest<ShopToStoreModel, ShopToStoreWorkflow>(Model, wfid);
        }

        public void ResubmitPurchaseToStore(PurchaseToStoreModel Model, Guid wfid)
        {
            ResubmitRequest<PurchaseToStoreModel, PurchaseToStoreWorkflow>(Model, wfid);
        }



        public void ApproveFactoryToStore(FactoryToStoreModel Model, Guid wfid)
        {
            PassContext(_transactionService, _context);
            var data = GetData<FactoryToStoreModel, FactoryToStoreWorkflow>(wfid);
            data.fprr = Model.fprr;
            data.fprrdocument = Model.fprrdocument;
            data.description = Model.description;
            ApproveMethod<FactoryToStoreModel, FactoryToStoreWorkflow>(data, wfid,_transactionService.TransferFromFactoryToStore);
        }

        public void ApproveStoreToShop(string description, Guid wfid)
        {
            PassContext(_transactionService, _context);
            ApproveMethod2<StoreToShopModel, StoreToShopWorkflow>(description, wfid, _transactionService.TransferFromStoreToShop);
        }

        public void ApprovePurchaseToStore(string description, Guid wfid)
        {
            PassContext(_transactionService, _context);
            ApproveMethod2<PurchaseToStoreModel, PurchaseToStoreWorkflow>(description, wfid, _transactionService.TransferFromPurchaseToStore);
        }

        public void ApproveShopToStore(string description, Guid wfid)
        {
            PassContext(_transactionService, _context);
            ApproveMethod2<ShopToStoreModel, ShopToStoreWorkflow>(description, wfid, _transactionService.TransfertFromShopToStore);
        }

        public void RejectFactoryToStore(string description, Guid wfid)
        {
            RejectMethod<FactoryToStoreModel, FactoryToStoreWorkflow>(description, wfid);
        }

        public void RejectStoreToShop(string description, Guid wfid)
        {
            RejectMethod<StoreToShopModel, StoreToShopWorkflow>(description, wfid);
        }

        public void RejectPurchaseToStore(string description, Guid wfid)
        {
            RejectMethod<PurchaseToStoreModel, PurchaseToStoreWorkflow>(description, wfid);
        }

        public void RejectShopToStore(string description, Guid wfid)
        {
            RejectMethod<ShopToStoreModel, ShopToStoreWorkflow>(description, wfid);
        }

        public void CancelFactoryToStore(string description, Guid wfid)
        {
            CancelMethod<FactoryToStoreModel, FactoryToStoreWorkflow>(description, wfid);
        }

        public void CancelStoreToShop(string description, Guid wfid)
        {
            CancelMethod<StoreToShopModel, StoreToShopWorkflow>(description, wfid);
        }

        public void CancelPurchaseToStore(string description, Guid wfid)
        {
            CancelMethod<PurchaseToStoreModel, PurchaseToStoreWorkflow>(description, wfid);
        }

        public void CancelShopToStore(string description, Guid wfid)
        {
            CancelMethod<ShopToStoreModel, ShopToStoreWorkflow>(description, wfid);
        }

        public void AddSalesReportTransaction(SalesReportModel Model)
        {
            Transact(t =>
            {
                PassContext(_transactionService, _context);
                _transactionService.AddSalesReportTransaction(Model);
            });
        }

        public FactoryToStore GetFactoryToStore(int id)
        {
            PassContext(_transactionService, _context);
            return _transactionService.GetFactoryToStore(id);
        }

        public StoreToShop GetStoreToShop(int id)
        {
            PassContext(_transactionService, _context);
            return _transactionService.GetStoreToShop(id);
        }

        public PurchaseToStore GetPurchaseToStore(int id)
        {
            PassContext(_transactionService, _context);
            return _transactionService.GetPurchaseToStore(id);
        }

        public ShopToStore GetShopToStore(int id)
        {
            PassContext(_transactionService, _context);
            return _transactionService.GetShopToStore(id);
        }

        public SalesReport GetSalesReport(int id)
        {
            PassContext(_transactionService, _context);
            return _transactionService.GetSalesReport(id);
        }
    }
}
