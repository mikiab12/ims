using System;
using System.Collections.Generic;

namespace ims.data.Entities
{
    public partial class Workflow
    {
        public Workflow()
        {
            Workitem = new HashSet<WorkItem>();
        }

        public Guid Id { get; set; }
        public int Currentstate { get; set; }
        public string Description { get; set; }
        public int Typeid { get; set; }
        public long? Aid { get; set; }
        public string Initiatoruser { get; set; }
        public int? Employeeid { get; set; }

        public string Observer { get; set; }
        public System.Nullable<Guid> Parentwfid { get; set; }

        public UserAction A { get; set; }
        public WorkflowType Type { get; set; }
        public ICollection<WorkItem> Workitem { get; set; }
    }

    public class WorkflowDocuments
    {
        public int Id { get; set; }
        public Guid Workflowid { get; set; }
        public string Filename { get; set; }
        public string Contentdisposition { get; set; }
        public string Contenttype { get; set; }
        public string Name { get; set; }
        public long Length { get; set; }
        public byte[] File { get; set; }
        public int? Index { get; set; }
    }

    public class WFSearchResultModel
    {
        public Guid Id { get; set; }
        public Guid WorkflowId { get; set; }
        public string Description { get; set; }
        public int SeqNo { get; set; }
        public int FromState { get; set; }
        public int Trigger { get; set; }
        public string DataType { get; set; }
        public string Data { get; set; }
        public int CurrentState { get; set; }
        public int TypeId { get; set; }
        public string initiatorUser { get; set; }
        public int? EmployeeID { get; set; }
        public string Observer { get; set; }
        public System.Nullable<Guid> parentWFID { get; set; }

    }
}
