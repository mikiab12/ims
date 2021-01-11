using ims.data;
using ims.data.Entities;
using ims.domain.Admin;
using ims.domain.Extensions;
using ims.domain.Infrastructure;
using ims.domain.Infrastructure.Architecture;
using ims.domain.Transaction.Models;
using ims.domain.Workflows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ims.domain.Transaction
{
    public interface iTransactionSearchSerivce : iIMSService
    {
        void SetSession(UserSession session);
        void SetConfiguration(IMSConfiguration Config);
        TransactionSearchResponse SearchTransaction(TransactionSearchRequest request);
        StockSearchResponse SearchStock(StockSearchRequest request);
    }

    public class TransactionSearchService : IMSService, iTransactionSearchSerivce
    {

        public StoreDbContext _context;
        private IMSConfiguration _config;
        private UserSession _session;
        private IWorkflowService _workflowService;
        private IUserService _userService;
        public TransactionSearchService(StoreDbContext context, IWorkflowService wfService, IUserService usService)
        {
            _context = context;
            _workflowService = wfService;
            _userService = usService;
            _workflowService.SetContext(context);
            SetContext(context);

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


        public TransactionSearchResponse SearchTransaction(TransactionSearchRequest request)
        {
            TransactionSearchResponse response = new TransactionSearchResponse();
            switch (request.Type)
            {
                case TransactionType.FactoryToStore:
                    response = SearchFactoryToStore(request);
                    break;
                case TransactionType.StoreToShop:
                    response = SearchStoreToShop(request);
                    break;
                case TransactionType.PurhcaseToStore:
                    response = SearchPurchaseToStore(request);
                    break;
                case TransactionType.ShopToStore:
                    response = SearchShopToStores(request);
                    break;
                case TransactionType.SalesReport:
                    response = SearchSalesReport(request);
                    break;
                default:
                    break;
            }
            return response;
        }

        public TransactionSearchResponse SearchFactoryToStore(TransactionSearchRequest request)
        {

            var fd = request.FromDate;
            var td = request.ToDate;
            var sql = $@"select * from public.factory_to_stores 
                        where date > @date1 and date < @date2  ";
            var factory = request.FactoryId != null ? " and factory_id = @factoryId " : " ";
            var storeId = request.StoreId != null ? " and store_id = @storeId " : " ";
            var fprr = !(String.IsNullOrWhiteSpace(request.fprr)) ? " and fprr = @fprr " : " ";
            var fptv = !(String.IsNullOrWhiteSpace(request.fptv)) ? " and fptv = @fptv " : " ";

            sql = $"{sql} {factory} {storeId} {fprr} {fptv} ;";

            Context.Database.OpenConnection();
            var connection = (Npgsql.NpgsqlConnection)Context.Database.GetDbConnection();
            var cmd = new Npgsql.NpgsqlCommand(sql, connection);
            //cmd.Prepare();
            cmd.Parameters.Add("@date1", NpgsqlTypes.NpgsqlDbType.Date).Value = new DateTime(fd.Year,fd.Month,fd.Day).AddDays(1);
            cmd.Parameters.Add("@date2", NpgsqlTypes.NpgsqlDbType.Date).Value = new DateTime(td.Year,td.Month,td.Day).AddDays(1);
            if (request.FactoryId != null)
            {
                cmd.Parameters.Add("@factoryId", NpgsqlTypes.NpgsqlDbType.Integer).Value = request.FactoryId;
            }
            if (request.StoreId != null)
            {
                cmd.Parameters.Add("@storeId", NpgsqlTypes.NpgsqlDbType.Integer).Value = request.StoreId;
            }
            if(!(String.IsNullOrWhiteSpace(request.fprr)))
            {
                cmd.Parameters.Add("@fprr", NpgsqlTypes.NpgsqlDbType.Text).Value = request.fprr;
            }
            if (!(String.IsNullOrWhiteSpace(request.fptv)))
            {
                cmd.Parameters.Add("@fptv", NpgsqlTypes.NpgsqlDbType.Text).Value = request.fptv;
            }
            List<FactoryToStore> res = new List<FactoryToStore>();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    res.Add(new FactoryToStore()
                    {
                        Id = dr.toInt(0),
                        TotalNumber = dr.toInt(1),
                        FPRR_ID = dr.toInt(2),
                        Date = dr.toDate(3),
                        FactoryId = dr.toInt(4),
                        StoreId = dr.toInt(5),
                        ConfirmedById = dr.toInt(6),
                        Description = dr[7].ToString(),
                        fprr = dr[8].ToString(),
                        fptv = dr[9].ToString(),
                        InitiatorId = dr.toInt(10),
                        FPTV_Id = dr.toInt(12)
                    });
                }
            }

            Context.Database.CloseConnection();
            return new TransactionSearchResponse()
            {
                FactoryToStore = res,
                Request = request
            };
        }

        public TransactionSearchResponse SearchStoreToShop(TransactionSearchRequest request)
        {

            var fd = request.FromDate;
            var td = request.ToDate;
            var sql = $@"select * from public.store_to_shops 
                        where date > @date1 and date < @date2 ";
            var shop = request.ShopId != null ? " and shop_id = @shopId " : " ";
            var storeId = request.StoreId != null ? " and store_id = @storeId " : " ";
            var dn = !(String.IsNullOrWhiteSpace(request.dn)) ? " and dn = @dn " : " ";

            sql = $"{sql} {shop} {storeId} {dn} ;";

            Context.Database.OpenConnection();
            var connection = (Npgsql.NpgsqlConnection)Context.Database.GetDbConnection();
            var cmd = new Npgsql.NpgsqlCommand(sql, connection);
            cmd.Parameters.Add("@date1", NpgsqlTypes.NpgsqlDbType.Date).Value = new DateTime(fd.Year, fd.Month, fd.Day).AddDays(1);
            cmd.Parameters.Add("@date2", NpgsqlTypes.NpgsqlDbType.Date).Value = new DateTime(td.Year, td.Month, td.Day).AddDays(2);
            if (request.ShopId != null)
            {
                cmd.Parameters.Add("@shopId", NpgsqlTypes.NpgsqlDbType.Integer).Value = request.ShopId;
            }
            if (request.StoreId != null)
            {
                cmd.Parameters.Add("@storeId", NpgsqlTypes.NpgsqlDbType.Integer).Value = request.StoreId;
            }
            if (!(String.IsNullOrWhiteSpace(request.dn)))
            {
                cmd.Parameters.Add("@dn", NpgsqlTypes.NpgsqlDbType.Text).Value = request.dn;
            }
            List<StoreToShop> res = new List<StoreToShop>();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    res.Add(new StoreToShop()
                    {
                        Id = dr.toInt(0),
                        StoreId = dr.toInt(1),
                        TotalNumber = dr.toInt(2),
                        DN_Id = dr.toInt(3),
                        ShopId = dr.toInt(4),
                        Description = dr[5].ToString(),
                        Date = dr.toDate(6),
                        ConfirmedById = dr.toInt(7),
                        dn = dr[8].ToString(),
                        InitiatorId = dr.toInt(9)
                    });
                }
            }

            Context.Database.CloseConnection();
            return new TransactionSearchResponse()
            {
                StoreToShop = res,
                Request = request
            };
        }

        public TransactionSearchResponse SearchSalesReport(TransactionSearchRequest request)
        {
            var fd = request.FromDate;
            var td = request.ToDate;

            var sql = $@"select * from public.sales_reports
                        where date_from >= '2020-01-01' and date_to <= '2020-03-03'";

            var shop = request.ShopId != null ? " and shop_id = @shopId " : " ";
            var srn = !(String.IsNullOrWhiteSpace(request.srn)) ? " and reference = @srn " : " ";
            sql = $"{sql} {shop} {srn}";
            Context.Database.OpenConnection();
            var connection = (Npgsql.NpgsqlConnection)Context.Database.GetDbConnection();
            var cmd = new Npgsql.NpgsqlCommand(sql, connection);
            cmd.Parameters.Add("@date1", NpgsqlTypes.NpgsqlDbType.Date).Value = new DateTime(fd.Year, fd.Month, fd.Day).AddDays(1);
            cmd.Parameters.Add("@date2", NpgsqlTypes.NpgsqlDbType.Date).Value = new DateTime(td.Year, td.Month, td.Day).AddDays(2);
            if (request.ShopId != null)
            {
                cmd.Parameters.Add("@shopId", NpgsqlTypes.NpgsqlDbType.Integer).Value = request.ShopId;
            }
            if (!(String.IsNullOrWhiteSpace(request.srn)))
            {
                cmd.Parameters.Add("@srn", NpgsqlTypes.NpgsqlDbType.Text).Value = request.srn;

            }
            List<SalesReport> rep = new List<SalesReport>();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    rep.Add(new SalesReport()
                    {
                        Id = dr.toInt(0),
                        ShopId = dr.toInt(1),
                        reference = dr[2].ToString(),
                        referenceId = dr.toInt(3),
                        reportedBy = dr.toInt(4),
                        totalNumber = dr.toInt(5),
                        description = dr[6].ToString(),
                        dateFrom = dr.toDate(7),
                        dateTo = dr.toDate(8)
                    });
                }
            }
            Context.Database.CloseConnection();
            return new TransactionSearchResponse()
            {
                Request = request,
                SalesReport = rep
            };

        }

        public StockSearchResponse SearchStock(StockSearchRequest request)
        {
            var response = new StockSearchResponse();
            switch (request.Type)
            {
                case StockSearchType.StoreStockType:
                    response = SearchStoreStock(request);
                    break;
                case StockSearchType.ShopStockType:
                    response = SearchShopStock(request);
                    break;
                default:
                    break;
            }
            return response;
        }

        public StockSearchResponse SearchStoreStock(StockSearchRequest Request)
        {
            var d = Request.Date;
            d = new DateTime(d.Year, d.Month, d.Day).AddDays(2);
            var shoeid = Request.Shoes != null && Request.Shoes.Length > 0 ? " and shoe_id = ANY(@shoeId)" : "";
            var sql = $@"select * from (select *, ROW_NUMBER() Over (Partition By shoe_id Order by seq_no desc)
                           as pos from public.store_stocks where store_id = @storeId and date < @date {shoeid} ) as s where s.pos = 1 ";

            Context.Database.OpenConnection();
            var connection = (Npgsql.NpgsqlConnection)Context.Database.GetDbConnection();
            var cmd = new Npgsql.NpgsqlCommand(sql, connection);

            cmd.Parameters.Add("@date", NpgsqlTypes.NpgsqlDbType.Date).Value = d;
            cmd.Parameters.Add("@storeId", NpgsqlTypes.NpgsqlDbType.Integer).Value = Request.StoreId;
            if (!(String.IsNullOrWhiteSpace(shoeid)))
            {
                cmd.Parameters.Add("@shoeId", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer).Value = Request.Shoes;
            }

            List<StoreStock> stk = new List<StoreStock>();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    stk.Add(new StoreStock()
                    {
                        Id = dr.toInt(0),
                        StoreId = dr.toInt(1),
                        ShoeId = dr.toInt(2),
                        Amount = dr.toInt(3),
                        Date = dr.toDate(4),
                        Stock = dr.toInt(5),
                        SeqNo = dr.toInt(6)
                    });
                }
            }
            Context.Database.CloseConnection();

            return new StockSearchResponse()
            {
                Request = Request,
                StoreStocks = stk
            };

        }

        public StockSearchResponse SearchShopStock(StockSearchRequest Request)
        {
            var d = Request.Date;
            d = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0).AddDays(2);
            var shoeid = Request.Shoes != null && Request.Shoes.Length > 0 ? " and shoe_id = ANY(@shoeId)" : "";
            var sql = $@"select * from (select *, ROW_NUMBER() Over (Partition By shoe_id Order by seq_no desc)
                           as pos from public.shop_stocks where shop_id = @shopId and date < @date {shoeid} ) as s where s.pos = 1 ";

            Context.Database.OpenConnection();
            var connection = (Npgsql.NpgsqlConnection)Context.Database.GetDbConnection();
            var cmd = new Npgsql.NpgsqlCommand(sql, connection);

            cmd.Parameters.Add("@date", NpgsqlTypes.NpgsqlDbType.Date).Value = d;
            cmd.Parameters.Add("@shopId", NpgsqlTypes.NpgsqlDbType.Integer).Value = Request.ShopId;
            if (!(String.IsNullOrWhiteSpace(shoeid)))
            {
                cmd.Parameters.Add("@shoeId", NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer).Value = Request.Shoes;
            }

            List<ShopStock> stk = new List<ShopStock>();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    stk.Add(new ShopStock()
                    {
                        Id = dr.toInt(0),
                        ShoeId = dr.toInt(1),
                        Amount = dr.toInt(2),
                        ShopId = dr.toInt(3),
                        Date = dr.toDate(4),
                        Stock = dr.toInt(5),
                        SeqNo = dr.toInt(6)
                    });
                }
            }
            Context.Database.CloseConnection();

            return new StockSearchResponse()
            {
                Request = Request,
                ShopStocks = stk
            };

        }

        public TransactionSearchResponse SearchPurchaseToStore(TransactionSearchRequest request)
        {

            var fd = request.FromDate;
            var td = request.ToDate;
            var sql = $@"select * from public.purchase_to_stores 
                        where date > @date1 and date < @date2 ";
            var supplier = request.SupplierId != null ? " and suppiler_id = @supplierId " : " ";
            var storeId = request.StoreId != null ? " and store_id = @storeId " : " ";
            var grn = !(String.IsNullOrWhiteSpace(request.grn)) ? " and grn = @grn " : " ";

            sql = $"{sql} {supplier} {storeId} {grn} ;";

            Context.Database.OpenConnection();
            var connection = (Npgsql.NpgsqlConnection)Context.Database.GetDbConnection();
            var cmd = new Npgsql.NpgsqlCommand(sql, connection);
            //cmd.Prepare();
            cmd.Parameters.Add("@date1", NpgsqlTypes.NpgsqlDbType.Date).Value = new DateTime(fd.Year, fd.Month, fd.Day).AddDays(1);
            cmd.Parameters.Add("@date2", NpgsqlTypes.NpgsqlDbType.Date).Value = new DateTime(td.Year, td.Month, td.Day).AddDays(2);
            if (request.SupplierId != null)
            {
                cmd.Parameters.Add("@supplierId", NpgsqlTypes.NpgsqlDbType.Integer).Value = request.SupplierId;
            }
            if (request.StoreId != null)
            {
                cmd.Parameters.Add("@storeId", NpgsqlTypes.NpgsqlDbType.Integer).Value = request.StoreId;
            }
            if (!(String.IsNullOrWhiteSpace(request.dn)))
            {
                cmd.Parameters.Add("@grn", NpgsqlTypes.NpgsqlDbType.Text).Value = request.grn;
            }
            List<PurchaseToStore> res = new List<PurchaseToStore>();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    res.Add(new PurchaseToStore()
                    {
                        Id = dr.toInt(0),
                        Date = dr.toDate(1),
                        StoreId = dr.toInt(2),
                        InitiatorId = dr.toInt(3),
                        ConfirmedById = dr.toInt(4),
                        TotalNumber = dr.toInt(6),
                        Description = dr[7].ToString(),
                        SuppilerId = dr.toInt(8),
                        InvoiceNumber = dr[9].ToString(),
                        grn = dr[11].ToString(),
                        GRN_Id = dr.toInt(12)
                    });
                }
            }

            Context.Database.CloseConnection();
            return new TransactionSearchResponse()
            {
                PurchaseToStore = res,
                Request = request
            };
        }

        public TransactionSearchResponse SearchShopToStores(TransactionSearchRequest request)
        {

            var fd = request.FromDate;
            var td = request.ToDate;
            var sql = $@"select * from public.shop_to_stores 
                        where date > @date1 and date < @date2 ";
            var shop = request.ShopId != null ? " and shop_id = @shopId " : " ";
            var storeId = request.StoreId != null ? " and store_id = @storeId " : " ";
            var mrn = !(String.IsNullOrWhiteSpace(request.dn)) ? " and mrn = @mrn " : " ";

            sql = $"{sql} {shop} {storeId} {mrn} ;";

            Context.Database.OpenConnection();
            var connection = (Npgsql.NpgsqlConnection)Context.Database.GetDbConnection();
            var cmd = new Npgsql.NpgsqlCommand(sql, connection);
            //cmd.Prepare();
            cmd.Parameters.Add("@date1", NpgsqlTypes.NpgsqlDbType.Date).Value = new DateTime(fd.Year, fd.Month, fd.Day).AddDays(1);
            cmd.Parameters.Add("@date2", NpgsqlTypes.NpgsqlDbType.Date).Value = new DateTime(td.Year, td.Month, td.Day).AddDays(2);
            if (request.ShopId != null)
            {
                cmd.Parameters.Add("@shopId", NpgsqlTypes.NpgsqlDbType.Integer).Value = request.ShopId;
            }
            if (request.StoreId != null)
            {
                cmd.Parameters.Add("@storeId", NpgsqlTypes.NpgsqlDbType.Integer).Value = request.StoreId;
            }
            if (!(String.IsNullOrWhiteSpace(request.mrn)))
            {
                cmd.Parameters.Add("@mrn", NpgsqlTypes.NpgsqlDbType.Text).Value = request.mrn;
            }
            List<ShopToStore> res = new List<ShopToStore>();
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    res.Add(new ShopToStore()
                    {
                        Id = dr.toInt(0),
                        Date = dr.toDate(1),
                        StoreId = dr.toInt(2),
                        InitiatorId = dr.toInt(3),
                        ConfirmedById = dr.toInt(4),
                        TotalNumber = dr.toInt(6),
                        Description = dr[7].ToString(),
                        ShopId = dr.toInt(8),
                        mrn = dr[9].ToString(),
                        MRN_Id = dr.toInt(10)
                    });
                }
            }

            Context.Database.CloseConnection();
            return new TransactionSearchResponse()
            {
                ShopToStore = res,
                Request = request
            };
        }
    }
}
