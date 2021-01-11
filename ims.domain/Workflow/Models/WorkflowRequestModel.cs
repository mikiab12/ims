using System;
using System.Collections.Generic;
using System.Text;

namespace ims.domain.Workflows.Models
{
    public class WorkflowRequest
    {
        public int CurrentState { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; } // not editable
        public int? EmployeeID { get; set; }
        public string initiatorUser { get; set; }
        public System.Nullable<Guid> parentWFID { get; set; }
        public string Observer { get; set; }


    }


    public class WorkItemRequest
    {
        public string WorkflowId { get; set; } // encapsulated in workflow definitions
        public int FromState { get; set; } // encapsulated in workflow definitions
        public int Trigger { get; set; } // encapsulated in workflow definitions
        public string DataType { get; set; } // encapsulated in workflow definitions
        public string Data { get; set; }
        public string Description { get; set; }
        public long? AssignedRole { get; set; } // encapsulated in workflow definitions
        public long? AssignedUser { get; set; }
    }
}
