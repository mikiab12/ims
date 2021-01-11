using System;
using System.Collections.Generic;
using System.Text;

namespace ims.domain.Workflows.Models
{
    public class WFSearchRequestModel
    {
        public WorkflowTypes Type { get; set; }
        public System.Nullable<DateTime> FromDate { get; set; }
        public System.Nullable<DateTime> ToDate { get; set; }
        public string Reference { get; set; }
        public string CurrentState { get; set; }
        public string Note { get; set; }
        public string EmployeeID { get; set; }
        public string Supplier { get; set; }
        public string Initiator { get; set; }
    }

   
}
