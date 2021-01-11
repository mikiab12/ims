using ims.data;
using ims.data.Entities;
using ims.domain.Admin;
using ims.domain.Infrastructure;
using ims.domain.Infrastructure.Architecture;
using ims.domain.Transaction.Models;
using ims.domain.Workflows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ims.domain.Transaction
{
    public interface ITransactionService : iIMSService
    {
        void SetSession(UserSession session);
        void SetConfiguration(IMSConfiguration Config);
        void TransferFromFactoryToStore(FactoryToStoreModel Model, Guid wfid);
        void TransferFromStoreToShop(StoreToShopModel Model, Guid wfid);
        void TransferFromPurchaseToStore(PurchaseToStoreModel Model, Guid wfid);
        void TransfertFromShopToStore(ShopToStoreModel Model, Guid wfid);
        void AddSalesReportTransaction(SalesReportModel Model);
        FactoryToStore GetFactoryToStore(int id);
        StoreToShop GetStoreToShop(int id);
        PurchaseToStore GetPurchaseToStore(int id);
        ShopToStore GetShopToStore(int id);
        SalesReport GetSalesReport(int id);
    }

    public class TransactionService : IMSService, ITransactionService
    {
        public StoreDbContext _context;
        private IMSConfiguration _config;
        private UserSession _session;
        private IWorkflowService _workflowService;
        private IUserService _userService;
        public TransactionService(StoreDbContext context, IWorkflowService wfService, IUserService usService)
        {
            _context = context;
            _workflowService = wfService;
            _userService = usService;
            _workflowService.SetContext(context);
            
        }

        public void SetSession(UserSession session)
        {
            _session = session;
            _workflowService.SetSession(session);
            _userService.SetSession(session);
        }

        public void SetConfiguration(IMSConfiguration Config)
        {
            _config = Config;
        }

        public void AddSalesReportTransaction(SalesReportModel Model)
        {
            Document reference = Model.referenceDoc;
            reference.type = DocumentTypes.SalesReport;
            _context.Documents.Add(reference);
            _context.SaveChanges();
            var dt = Model.dateTo;
            var df = Model.dateFrom;
            SalesReport rep = new SalesReport()
            {
                reference = Model.reference,
                dateFrom = new DateTime(df.Year,df.Month,df.Day,0,0,0),
                dateTo = new DateTime(dt.Year,dt.Month,dt.Day,23,59,59),
                description = Model.description,
                referenceId = reference.ID,
                reportedBy = _session.Id,
                ShopId = Model.shopId,
                totalNumber = Model.shoeList.Sum(m => m.Quantity)
            };
            _context.SalesReports.Add(rep);
            _context.SaveChanges();

            Model.shoeList.ForEach(m =>
            {
                m.TransactionId = rep.Id;
                m.TransactionType = TransactionTypes.SalesReport;
                _context.ShoeTransactionLists.Add(m);
            });
            _context.SaveChanges();

            UpdateShopStock(Model.shoeList, Model.shopId, false);
        }

        public void TransferFromFactoryToStore(FactoryToStoreModel Model, Guid wfid)
        {
            var wf = _workflowService.GetWorkflow(wfid);
            

            Document fptv = Model.fptvdocument;
            fptv.type = DocumentTypes.FPTV;
            Document fprr = Model.fprrdocument;
            fprr.type = DocumentTypes.FPRR;
            _context.Documents.Add(fptv);
            _context.Documents.Add(fprr);

            _context.SaveChanges();

            FactoryToStore fts = new FactoryToStore()
            {
                Date = DateTime.Now,
                Description = Model.description,
                FactoryId = Model.factoryId,
                StoreId = Model.storeId,
                fprr = Model.fprr,
                fptv = Model.fptv,
                FPRR_ID = fprr.ID,
                FPTV_Id = fptv.ID,
                WorkflowId = wfid,
                TotalNumber = Model.shoeList.Sum(m => m.Quantity),
                InitiatorId = _userService.GetUser(wf.Action.Username).Id,
                ConfirmedById = _session.Id,
                //ShoeList = Model.shoeList
            };
            
            _context.FactoryToStores.Add(fts);
            _context.SaveChanges();
            Model.shoeList.ForEach(m =>
            {
                m.TransactionId = fts.Id;
                m.TransactionType = TransactionTypes.FactoryToStore;
                _context.ShoeTransactionLists.Add(m);
            });
            _context.SaveChanges();
            UpdateStoreStock(Model.shoeList, Model.storeId, true);


        }

        public void TransferFromPurchaseToStore(PurchaseToStoreModel Model, Guid wfid)
        {
            var wf = _workflowService.GetWorkflow(wfid);
            Document grn = Model.grndocument;
            grn.type = DocumentTypes.GRN;
            _context.Documents.Add(grn);
            _context.SaveChanges();


            PurchaseToStore pts = new PurchaseToStore()
            {
                Date = DateTime.Now,
                Description = Model.description,
                SuppilerId = Model.supplierId,
                grn = Model.grn,
                GRN_Id = grn.ID,
                InvoiceNumber = Model.invoiceNo,
                StoreId = Model.storeId,
                TotalNumber = Model.shoeList.Sum(m => m.Quantity),
                WorkflowId = wfid,
                InitiatorId = _userService.GetUser(wf.Action.Username).Id,
                ConfirmedById = _session.Id,
            };
            _context.PurchaseToStores.Add(pts);
            _context.SaveChanges();
            Model.shoeList.ForEach(m =>
            {
                m.TransactionId = pts.Id;
                m.TransactionType = TransactionTypes.PurchaseToStore;
                _context.ShoeTransactionLists.Add(m);
            });
            UpdateStoreStock(Model.shoeList, Model.storeId, true);
        }

        public void TransferFromStoreToShop(StoreToShopModel Model, Guid wfid)
        {
            var wf = _workflowService.GetWorkflow(wfid);
            Document dn = Model.dndocument;
            dn.type = DocumentTypes.DN;
            _context.Documents.Add(dn);
            _context.SaveChanges();

            StoreToShop stsh = new StoreToShop()
            {
                Date = DateTime.Now,
                Description = Model.description,
                StoreId = Model.storeId,
                ShopId = Model.shopId,
                dn = Model.dn,
                DN_Id = dn.ID,
                TotalNumber = Model.shoeList.Sum(m => m.Quantity),
                WorkflowId = wfid,
                InitiatorId = _userService.GetUser(wf.Action.Username).Id,
                ConfirmedById = _session.Id,
            };
            _context.StoreToShops.Add(stsh);
            _context.SaveChanges();
            Model.shoeList.ForEach(m =>
            {
                m.TransactionId = stsh.Id;
                m.TransactionType = TransactionTypes.StoreToShop;
                _context.ShoeTransactionLists.Add(m);
            });

            UpdateStoreStock(Model.shoeList, Model.storeId, false);
            UpdateShopStock(Model.shoeList, Model.shopId, true);
        }

        public void TransfertFromShopToStore(ShopToStoreModel Model, Guid wfid)
        {
            var wf = _workflowService.GetWorkflow(wfid);
            Document mrn = Model.mrndocument;
            mrn.type = DocumentTypes.MRN;
            _context.Documents.Add(mrn);
            _context.SaveChanges();

            ShopToStore shst = new ShopToStore()
            {
                Date = DateTime.Now,
                Description = Model.description,
                StoreId = Model.storeId,
                ShopId = Model.shopId,
                mrn = Model.mrn,
                MRN_Id = mrn.ID,
                TotalNumber = Model.shoeList.Sum(m => m.Quantity),
                WorkflowId = wfid,
                InitiatorId = _userService.GetUser(wf.Action.Username).Id,
                ConfirmedById = _session.Id,
            };
            _context.ShopToStores.Add(shst);
            _context.SaveChanges();
            Model.shoeList.ForEach(m =>
            {
                m.TransactionId = shst.Id;
                m.TransactionType = TransactionTypes.PurchaseToStore;
                _context.ShoeTransactionLists.Add(m);
            });
            UpdateStoreStock(Model.shoeList, Model.storeId, true);
            UpdateShopStock(Model.shoeList, Model.shopId, false);
        }

        public void UpdateStoreStock(List<ShoeTransactionList> shoeList, int StoreId, bool incoming)
        {
            foreach (var shoe in shoeList)
            {
                var count = _context.StoreStocks.Where(m => m.StoreId == StoreId && m.ShoeId == shoe.ShoeId).Count();
                if(count > 0)
                {
                    var stock = _context.StoreStocks.Where(m => m.StoreId == StoreId && m.ShoeId == shoe.ShoeId).OrderBy(m => m.Id).Last();
                    if (DateTime.Today == new DateTime(stock.Date.Year, stock.Date.Month, stock.Date.Day))
                    {
                        if (incoming)
                        {
                            stock.Amount += shoe.Quantity;
                            stock.Stock += shoe.Quantity;
                        }
                        else
                        {
                            stock.Amount -= shoe.Quantity;
                            stock.Stock -= shoe.Quantity;
                        }
                        stock.Date = DateTime.Now;
                        _context.StoreStocks.Update(stock);
                    }
                    else
                    {
                        var newStock = new StoreStock()
                        {
                            StoreId = StoreId,
                            Date = DateTime.Now,
                            Amount = shoe.Quantity,
                            ShoeId = shoe.ShoeId,
                            SeqNo = stock.SeqNo + 1
                        };
                        if (incoming) { newStock.Stock = stock.Stock + shoe.Quantity; }
                        else { newStock.Stock = stock.Stock - shoe.Quantity; }
                        _context.StoreStocks.Add(newStock);
                    }
                }
                else
                {
                    var newStock = new StoreStock()
                    {
                        StoreId = StoreId,
                        Date = DateTime.Now,
                        Amount = shoe.Quantity,
                        ShoeId = shoe.ShoeId,
                        SeqNo = 1
                    };
                    if (incoming) { newStock.Stock = shoe.Quantity; }
                    else { newStock.Stock = -shoe.Quantity; }
                    _context.StoreStocks.Add(newStock);
                }
            }
            _context.SaveChanges();
        }

        public void UpdateShopStock(List<ShoeTransactionList> shoeList, int ShopId, bool incoming)
        {
            foreach (var shoe in shoeList)
            {
                var count = _context.ShopStocks.Where(m => m.ShopId == ShopId && m.ShoeId == shoe.ShoeId).Count();
                if (count > 0)
                {
                    var stock = _context.ShopStocks.Where(m => m.ShopId == ShopId && m.ShoeId == shoe.ShoeId).OrderBy(m => m.Id).Last();
                    if (DateTime.Today == new DateTime(stock.Date.Year, stock.Date.Month, stock.Date.Day))
                    {
                        if (incoming)
                        {
                            stock.Amount += shoe.Quantity;
                            stock.Stock += shoe.Quantity;
                        }
                        else
                        {
                            stock.Amount -= shoe.Quantity;
                            stock.Stock -= shoe.Quantity;
                        }
                        stock.Date = DateTime.Now;
                        _context.ShopStocks.Update(stock);
                    }
                    else
                    {
                        var newStock = new ShopStock()
                        {
                            ShopId = ShopId,
                            Date = DateTime.Now,
                            Amount = shoe.Quantity,
                            ShoeId = shoe.ShoeId,
                            SeqNo = stock.SeqNo + 1
                        };
                        if (incoming) { newStock.Stock = stock.Stock + shoe.Quantity; }
                        else { newStock.Stock = stock.Stock - shoe.Quantity; }
                        _context.ShopStocks.Add(newStock);
                    }
                }
                else
                {
                    var newStock = new ShopStock()
                    {
                        ShopId = ShopId,
                        Date = DateTime.Now,
                        Amount = shoe.Quantity,
                        ShoeId = shoe.ShoeId,
                        SeqNo = 1
                    };
                    if (incoming) { newStock.Stock = shoe.Quantity; }
                    else { newStock.Stock = -shoe.Quantity; }
                    _context.ShopStocks.Add(newStock);
                }
            }
            _context.SaveChanges();
        }

        public FactoryToStore GetFactoryToStore(int id)
        {
            var ft = _context.FactoryToStores.Find(id);
            ft.ShoeList = _context.ShoeTransactionLists.Where(m => m.TransactionId == id && m.TransactionType == TransactionTypes.FactoryToStore).ToList();
            return ft;
        }

        public StoreToShop GetStoreToShop(int id)
        {
            var stsh =  _context.StoreToShops.Find(id);
            stsh.ShoeList = _context.ShoeTransactionLists.Where(m => m.TransactionId == id && m.TransactionType == TransactionTypes.StoreToShop).ToList();
            return stsh;
        }

        public PurchaseToStore GetPurchaseToStore(int id)
        {
            var pts =  _context.PurchaseToStores.Find(id);
            pts.ShoeList = _context.ShoeTransactionLists.Where(m => m.TransactionId == id && m.TransactionType == TransactionTypes.PurchaseToStore).ToList();
            return pts;
        }

        public ShopToStore GetShopToStore(int id)
        {
            var shst = _context.ShopToStores.Find(id);
            shst.ShoeList = _context.ShoeTransactionLists.Where(m => m.TransactionId == id && m.TransactionType == TransactionTypes.ShopToStore).ToList();
            return shst;
        }

        public SalesReport GetSalesReport(int id)
        {
            var sr = _context.SalesReports.Find(id);
            sr.ShoeList = _context.ShoeTransactionLists.Where(m => m.TransactionId == id && m.TransactionType == TransactionTypes.SalesReport).ToList();
            return sr;
        }


    }
}
