using System;
using System.Collections.Generic;
using System.Text;

namespace ims.domain.Workflows.Models
{
    //public class WorkflowHistoryItem
    //{
    //    public PWF.data.Entities.Workflow Workflow { get; set; }
    //    public DateTime Date { get; set; }
    //    public string Action { get; set; }
    //    public long AID { get; set; }
    //    public string Description { get; set; }
    //    public string Name { get; set; }
    //    public string key { get; set; }
    //    public string username { get; set; }
    //    public PWF.data.Entities.WorkItem workitem { get; set; }

    //}

    //public class WorkflowHistoryModel
    //{
    //    public PWF.data.Entities.Workflow Workflow { get; set; }
    //    public List<WorkflowHistoryItem> History { get; set; }

    //}

    //public static class TriggerActionMapper
    //{
    //    public static Dictionary<string,string> GetPaymentRequestWorkflowMapper()
    //    {
    //        Dictionary<string, string> Map = new Dictionary<string, string>();
    //        Map.Add("01", "Payment Requested By ");
    //        Map.Add("09", "Request Cancelled By ");
    //        Map.Add("015", "Request Aborted during closing of purchase order by ");
    //        Map.Add("15", "Request Checked By (Finance Manager) ");
    //        Map.Add("16", "Request Checked and Approved By (Finance Manager) ");
    //        Map.Add("13", "Request Rejected By (Finance Manager) ");
    //        Map.Add("27", "Request Approved By (Owner) ");
    //        Map.Add("24", "Request Rejected By (Owner) ");
    //        Map.Add("32", "Document Added By (Executer) ");
    //        Map.Add("312", "Document Removed By (Executer)");
    //        Map.Add("313", "Document Attached By (Executer)");
    //        Map.Add("314", "Document Detached By (Executer)");
    //        Map.Add("38", "Request Closed By (Executer) ");
    //        Map.Add("411", "Final Check Rejected By (Finalizer) ");
    //        Map.Add("410", " Request Finalized By (Finalizer) ");

    //        return Map;
    //    }

    //    public static string GetPaymentAction(int state, int trigger)
    //    {
    //        var map = GetPaymentRequestWorkflowMapper();
    //        var key = $"{state}{trigger}";
    //        return map[key];
    //    }

    //    public static Dictionary<string,string> GetPurchaseWorkflowMapper()
    //    {
    //        Dictionary<string, string> Map = new Dictionary<string, string>();
    //        Map.Add("01", "Purchase Requested By (Purchase Officer) ");
    //        Map.Add("04", "Purchase Cancelled By (Purchase Officer) ");
    //        Map.Add("12", "Purchase Approved By (Purchase Supervisor) ");
    //        Map.Add("13", "Purchase Rejected By (Purchase Supervisor) ");
    //        Map.Add("25", "Quotations Added By (Purchase Officer) ");
    //        Map.Add("26", "Quotations Submitted By (Purchase Officer) ");
    //        Map.Add("37", "Purchase Order Issued By (Purchase Processor) ");
    //        Map.Add("311", "Purchase Order Cancelled By (Purchase Processor) ");
    //        Map.Add("38", "Purchase Request Closed By (Purchase Processor) ");
    //        Map.Add("49", "Purchase Request Finalized By (Purchase Supervisor) ");
    //        Map.Add("410", "Purchase Request Finalization Rejected By (Purchase Supervisor) ");

    //        return Map;

    //    }

    //    public static string GetPurchaseAction(int state, int trigger)
    //    {
    //        var map = GetPurchaseWorkflowMapper();
    //        var key = $"{state}{trigger}";
    //        return map[key];
    //    }

    //    public static Dictionary<string,string> GetPurchaseOrderWorkflowMapper()
    //    {
    //        Dictionary<string, string> Map = new Dictionary<string, string>();
    //        Map.Add("01", "Purchase Order Issued By (Purchase Processor) ");
    //        Map.Add("010", "Purchase Order Cancelled By (Purchase Processor) ");
    //        Map.Add("12", "Purchase Order Approved By (Purchase Supervisor) ");
    //        Map.Add("13", "Purchase Order Rejected By (Purchase Supervisor) ");
    //        Map.Add("25", "Item Delivery Request Submitted By (Purchase Processor) ");
    //        Map.Add("211", "Service Certification Requested By (Purchase Processor) ");
    //        Map.Add("24", "Payment Request Submitted By (Purchase Processor) ");
    //        Map.Add("26", "Update to Payment Request Made by ");
    //        Map.Add("27", "Update to Item Delivery Made by ");
    //        Map.Add("212", "Update to Service Delivery Made by ");
    //        Map.Add("29", "Purchase Order Closed By (Purchase Processor)");
    //        Map.Add("28", "Purchase Order Terminated By (Purchase Processor)");


    //        return Map;
    //    }

    //    public static string GetPurchaseOrderAction(int state, int trigger)
    //    {
    //        var map = GetPurchaseOrderWorkflowMapper();
    //        var key = $"{state}{trigger}";
    //        return map[key];
    //    }



    //    public static Dictionary<string,string> GetStoreRequestWorkflowMapper()
    //    {
    //        Dictionary<string, string> Map = new Dictionary<string, string>();
    //        Map.Add("02", "Store Request Submitted By ");
    //        Map.Add("01", "Store Request Cancelled By ");
    //        Map.Add("13", "Store Request Processed (Reorganized) By (Store Keeper) ");
    //        Map.Add("18", "Store Request Rejected By (Store Keeper) ");
    //        Map.Add("24", "Store Request Approved By (Store Manager) ");
    //        Map.Add("25", "Store Request Rejected By (Store Manager) ");
    //        Map.Add("36", "Items Issued By (Store Keeper) ");
    //        Map.Add("37", "Store Request Closed By (Store Keeper) ");
    //        return Map;
    //    }


    //    public static string GetStoreRequestAction(int state, int trigger)
    //    {
    //        var map = GetStoreRequestWorkflowMapper();
    //        var key = $"{state}{trigger}";
    //        return map[key];
    //    }
    //}
}
