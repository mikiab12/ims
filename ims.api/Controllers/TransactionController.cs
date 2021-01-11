using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ims.domain.Documents;
using ims.domain.Transaction;
using ims.domain.Transaction.Models;
using ims.domain.Workflows;
using Microsoft.AspNetCore.Mvc;




namespace ims.api.Controllers
{

    [ErrorHandlingFilter]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionController : BaseController
    {
        private iTransactionFacade _transactionFacade;
        private IWorkflowFacade _workflowFacade;
        private iTransactionSearchSerivce _searchService;
        private IDocumentService _docService;
        public TransactionController(iTransactionFacade transactionFacade,IWorkflowFacade  wfFacade,
            iTransactionSearchSerivce searchService, IDocumentService docService)
        {
            _transactionFacade = transactionFacade;
            _workflowFacade = wfFacade;
            _searchService = searchService;
            _docService = docService;
        }

        [HttpGet]
        public IActionResult GetUserWorkflows()
        {
            _workflowFacade.SetSession(GetSession());
            var resp = _workflowFacade.GetUserWorkflows();
            return SuccessfulResponse(resp);
        }

        [HttpGet]
        public IActionResult GetWorkflow(Guid wfid)
        {
            _workflowFacade.SetSession(GetSession());
            var response = _workflowFacade.GetWorkflow(wfid);
            return SuccessfulResponse(response);
        }

        public IActionResult GetLasWorkItem(Guid wfid)
        {
            _workflowFacade.SetSession(GetSession());
            var resp = _workflowFacade.GetLastWorkItem(wfid);
            return SuccessfulResponse(resp);
        }

        [HttpPost]
        public IActionResult RequestFactoryToStoreTransfer(FactoryToStoreModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.RequestFactoryToStoreTransfer(Model);
            return SuccessfulResponse(true);
        }

        [HttpPost]
        public IActionResult RequestStoreToShop(StoreToShopModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.RequestStoreToShop(Model);
            return SuccessfulResponse(true);
        }

        [HttpPost]
        public IActionResult RequestPurchaseToStore(PurchaseToStoreModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.RequestPurchaseToStore(Model);
            return SuccessfulResponse(true);
        }


        [HttpPost]
        public IActionResult RequestShopToStore(ShopToStoreModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.RequestShopToStore(Model);
            return SuccessfulResponse(true);
        }

        [HttpPost]
        public IActionResult ApproveStoreToShop(TransactionRequestModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.ApproveStoreToShop(Model.description,Model.wfid);
            return SuccessfulResponse(true);
        }


        [HttpPost]
        public IActionResult ApproveFactoryToStore(FactoryToStoreModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.ApproveFactoryToStore(Model, Model.wfid);
            return SuccessfulResponse(true);
        }




        [HttpPost]
        public IActionResult ApprovePurchaseToStore(TransactionRequestModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.ApprovePurchaseToStore(Model.description, Model.wfid);
            return SuccessfulResponse(true);
        }



        [HttpPost]
        public IActionResult ApproveShopToStore(TransactionRequestModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.ApproveShopToStore(Model.description, Model.wfid);
            return SuccessfulResponse(true);
        }



        [HttpPost]
        public IActionResult RejectFactoryToStore(TransactionRequestModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.RejectFactoryToStore(Model.description, Model.wfid);
            return SuccessfulResponse(true);
        }



        [HttpPost]
        public IActionResult RejectStoreToShop(TransactionRequestModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.RejectStoreToShop(Model.description, Model.wfid);
            return SuccessfulResponse(true);
        }



        [HttpPost]
        public IActionResult RejectPurchaseToStore(TransactionRequestModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.RejectPurchaseToStore(Model.description, Model.wfid);
            return SuccessfulResponse(true);
        }


        [HttpPost]
        public IActionResult RejectShopToStore(TransactionRequestModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.RejectShopToStore(Model.description, Model.wfid);
            return SuccessfulResponse(true);
        }

        [HttpPost]
        public IActionResult CancelFactoryToStore(TransactionRequestModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.CancelFactoryToStore(Model.description, Model.wfid);
            return SuccessfulResponse(true);
        }


        [HttpPost]
        public IActionResult CancelStoreToShop(TransactionRequestModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.CancelStoreToShop(Model.description, Model.wfid);
            return SuccessfulResponse(true);
        }


        [HttpPost]
        public IActionResult CancelPurchaseToStore(TransactionRequestModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.CancelPurchaseToStore(Model.description, Model.wfid);
            return SuccessfulResponse(true);
        }


        [HttpPost]
        public IActionResult CancelShopToStore(TransactionRequestModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.CancelShopToStore(Model.description, Model.wfid);
            return SuccessfulResponse(true);
        }

        [HttpPost]
        public IActionResult ResubmitFactoryToStore(FactoryToStoreModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.ResubmitFactoryToStore(Model, Model.wfid);
            return SuccessfulResponse(true);
        }

        [HttpPost]
        public IActionResult ResubmitStoreToShop(StoreToShopModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.ResubmitStoreToShop(Model, Model.wfid);
            return SuccessfulResponse(true);
        }

        [HttpPost]
        public  IActionResult ResubmitShopToStore(ShopToStoreModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.ResubmitShopToStore(Model, Model.wfid);
            return SuccessfulResponse(true);
        }

        [HttpPost]
        public IActionResult ResubmitPurchaseToStore(PurchaseToStoreModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.ResubmitPurchaseToStore(Model, Model.wfid);
            return SuccessfulResponse(true);
        }

        [HttpPost]
        public IActionResult SearchTransaction(TransactionSearchRequest Model)
        {
            _searchService.SetSession(GetSession());
            var resp = _searchService.SearchTransaction(Model);
            return SuccessfulResponse(resp);
        }

        [HttpPost]
        public IActionResult SearchStock(StockSearchRequest Request)
        {
            _searchService.SetSession(GetSession());
            var resp = _searchService.SearchStock(Request);
            return SuccessfulResponse(resp);
        }

        [HttpPost]
        public IActionResult AddSalesReportTransaction(SalesReportModel Model)
        {
            _transactionFacade.SetSession(GetSession());
            _transactionFacade.AddSalesReportTransaction(Model);
            return SuccessfulResponse(true);
        }

        [HttpGet]
        public IActionResult GetFactoryToStore(int id)
        {
            _transactionFacade.SetSession(GetSession());
            var res = _transactionFacade.GetFactoryToStore(id);
            return SuccessfulResponse(res);
        }

        [HttpGet]
        public IActionResult GetStoreToShop(int id)
        {
            _transactionFacade.SetSession(GetSession());
            var res = _transactionFacade.GetStoreToShop(id);
            return SuccessfulResponse(res);
        }

        [HttpGet]
        public IActionResult GetPurchaseToStore(int id)
        {
            _transactionFacade.SetSession(GetSession());
            var res = _transactionFacade.GetPurchaseToStore(id);
            return SuccessfulResponse(res);
        }

        [HttpGet]
        public IActionResult GetShopToStore(int id)
        {
            _transactionFacade.SetSession(GetSession());
            var res = _transactionFacade.GetShopToStore(id);
            return SuccessfulResponse(res);
        }

        [HttpGet]
        public IActionResult GetSalesReport(int id)
        {
            _transactionFacade.SetSession(GetSession());
            var res = _transactionFacade.GetSalesReport(id);
            return SuccessfulResponse(res);
        }

        [HttpGet]
        public IActionResult GetDocument(int id)
        {
            _docService.SetSession(GetSession());
            var res = _docService.GetDocument(id);
            return SuccessfulResponse(res);
        }

        [HttpGet]
        public IActionResult GetTransaction(TransactionType type, int id)
        {
            _transactionFacade.SetSession(GetSession());
            switch (type)
            {
                case TransactionType.FactoryToStore:
                    return GetFactoryToStore(id);
                    break;
                case TransactionType.StoreToShop:
                    return GetStoreToShop(id);
                    break;
                case TransactionType.PurhcaseToStore:
                    return GetPurchaseToStore(id);
                    break;
                case TransactionType.ShopToStore:
                    return GetShopToStore(id);
                    break;
                case TransactionType.SalesReport:
                    return GetSalesReport(id);
                    break;
                default:
                    return SuccessfulResponse(null);
                    break;
            }
        }



    }
}