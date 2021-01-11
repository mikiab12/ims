using System;

namespace ims.data.Entities
{
    public partial class WorkItem
    {
        public Guid Id { get; set; }
        public Guid WorkflowId { get; set; }
        public int Seqno { get; set; }
        public int Fromstate { get; set; }
        public int Trigger { get; set; }
        public string Datatype { get; set; }
        public string Data { get; set; }
        public string Description { get; set; }
        public long? Assignedrole { get; set; }
        public long? Assigneduser { get; set; }
        public long? Aid { get; set; }
        public long? Assignedrolenavigationid { get; set; }
        public long? Assignedusernavigationid { get; set; }


        public UserAction A { get; set; }
        public Role Assignedrolenavigation { get; set; }
        public User Assignedusernaviagtion { get; set; }
        public Workflow Workflow { get; set; }
    }
}
