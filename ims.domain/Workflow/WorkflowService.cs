using ims.domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using ims.domain.Infrastructure.Architecture;
using ims.domain.Workflows.Models;
using System.Linq;
using ims.domain.Admin;
using Newtonsoft.Json;
using System.Globalization;
using ims.domain.Extensions;
using ims.data.Entities;

namespace ims.domain.Workflows
{
    public interface IWorkflowService : iIMSService
    {
        void SetSession(UserSession session);

        Workflow CreateWorkflow(WorkflowRequest request, string initiatorUser = "");
        Workflow UpdateWorkflow(Guid id, int currentState, string description);
        WorkflowResponse GetWorkflow(Guid id);
        IList<WorkflowResponse> GetUserWorkflows(UserSession session);

        WorkItem CreateWorkItem(WorkItemRequest request);
        IList<WorkItemResponse> GetWorkItems(Guid workflowId);
        WorkItemResponse GetLastWorkItem(Guid workflowId);

        int GetWorkflowState(Guid workItemId);
        Guid GetWorkflowId(Guid workItemId);
        Type GetWorkflowModelType(WorkflowTypes type);
        object GetWorkflowViewModel(Guid WorkflowId);
     //   WorkflowResubmissionModel GetWorkflowResubmissionModel(Guid WorkflowId);
        WorkflowDocuments GetWorkflowDocument(int id);
        List<WorkflowDocuments> GetWorkflowDocuments(Guid WorkflowId);
        void DeleteWorkflowDocument(int id);

     //   EmployeeInformationViewModel GetEmployeeInformationViewModel(Guid WorkflowId);

    }

    public class WorkflowService : IMSService, IWorkflowService
    {
        private UserSession _session;


        public void SetSession(UserSession session)
        {
            _session = session;
        }


        public Workflow CreateWorkflow(WorkflowRequest request,string initiatorUser = "")
        {
            var workflow = new Workflow
            {
                Id = Guid.NewGuid(),
                Currentstate = request.CurrentState,
                Description = request.Description,
                Typeid = request.TypeId,
                Employeeid = request.EmployeeID,
                Observer = request.Observer,
                Parentwfid = request.parentWFID
                
            };
            workflow.Initiatoruser = String.IsNullOrWhiteSpace(initiatorUser) ?_session.Username : workflow.Initiatoruser = initiatorUser;
            Context.Workflow.Add(workflow);

            workflow.Aid = Context.SaveChanges(_session.Username, (int)UserActionType.AddWorkflow).Id;
            Context.Workflow.Update(workflow);

            return workflow;
        }

        public Workflow UpdateWorkflow(Guid id, int currentState, string description)
        {
            var workflow = Context.Workflow.First(wf => wf.Id == id);

            workflow.Currentstate = currentState;
            workflow.Description = description;

            Context.Workflow.Update(workflow);

            workflow.Aid = Context.SaveChanges(_session.Username, (int)UserActionType.UpdateWorkflow).Id;
            Context.Workflow.Update(workflow);

            return workflow;
        }

        public WorkflowResponse GetWorkflow(Guid id)
        {
            var workflow = Context.Workflow.Where(wf => wf.Id == id).ToList().First();
            var wfType = Context.WorkflowType.Find(workflow.Typeid);
            workflow.Type = wfType;
            var action = Context.UserAction.First(ua => ua.Id == workflow.Aid);
            return new WorkflowResponse
            {
                Id = workflow.Id,
                CurrentState = workflow.Currentstate,
                Description = workflow.Description,
                TypeId = workflow.Typeid,
                EmployeeID = workflow.Employeeid,
                Type = new WorkflowTypeResponse
                {
                    Id = workflow.Type.Id,
                    Name = workflow.Type.Name,
                    Description = workflow.Type.Description
                },
                Observer = workflow.Observer,
                parentWFID = workflow.Parentwfid,
                Action = new WorkflowActionResponse
                {
                    Id = action.Id,
                    Username = action.Username,
                    TimeStr = action.Timestamp != null
                            ? new DateTime(action.Timestamp.Value).ToString(CultureInfo.InvariantCulture)
                            : null
                }

            };
        }

        public IList<WorkflowResponse> GetUserWorkflows(UserSession session)
        {

            var ret = new List<WorkflowResponse>();

            var workflowIds = new List<Guid>();
            var user = Context.User.Where(m => m.Username == session.Username).First();


            var workItems = Context.WorkItem.Where(wi =>
                wi.Workflow.Currentstate >= 0 && ( // the convention: final states have negative number values
                   // wi.Assigneduser != null && wi.Assigneduser == user.Id ||
                    wi.Assignedrole != null && wi.Assignedrole == session.Role)
            );

            foreach (var workItem in workItems)
            {
                if (workflowIds.Contains(workItem.WorkflowId)) continue;

                var workflow = Context.Workflow.First(wf => wf.Id == workItem.WorkflowId);

                var workflowType = Context.WorkflowType.First(wt => wt.Id == workflow.Typeid);
                var action = Context.UserAction.First(ua => ua.Id == workflow.Aid);

                workflowIds.Add(workflow.Id);

                ret.Add(new WorkflowResponse
                {
                    Id = workflow.Id,
                    CurrentState = workflow.Currentstate,
                    Description = workflow.Description,
                    TypeId = workflow.Typeid,
                    Aid = workflow.Aid,
                    Observer = workflow.Observer,
                    parentWFID = workflow.Parentwfid,
                    Type = new WorkflowTypeResponse
                    {
                        Id = workflowType.Id,
                        Name = workflowType.Name,
                        Description = workflowType.Description
                    },
                    Action = new WorkflowActionResponse
                    {
                        Id = action.Id,
                        Username = action.Username,
                        TimeStr = action.Timestamp != null
                            ? new DateTime(action.Timestamp.Value).ToString(CultureInfo.InvariantCulture)
                            : null
                    }
                });
            }

            return ret.OrderBy(wf => Context.UserAction.FirstOrDefault(ua => ua.Id == wf.Aid)?.Timestamp)
                .ToList();
        }


        public WorkItem CreateWorkItem(WorkItemRequest request)
        {
            var workItem = new WorkItem
            {
                Id = Guid.NewGuid(),
                WorkflowId = request.WorkflowId.ToGuid(),
                Seqno = GetMaxSeq(request.WorkflowId.ToGuid()) + 1,
                Fromstate = request.FromState,
                Trigger = request.Trigger,
                Datatype = request.DataType,
                Data = request.Data,
                Description = request.Description,
                Assignedrole = request.AssignedRole,
                Assigneduser = request.AssignedUser
            };

            Context.WorkItem.Add(workItem);

            workItem.Aid = Context.SaveChanges(_session.Username, (int)UserActionType.AddWorkItem).Id;
            Context.WorkItem.Update(workItem);

            return workItem;
        }

        public IList<WorkItemResponse> GetWorkItems(Guid workflowId)
        {
            var workItems = Context.WorkItem.Where(wi => wi.WorkflowId == workflowId)
                .OrderBy(wi => wi.Seqno);

            return workItems.Select(workItem => new WorkItemResponse
            {
                Id = workItem.Id,
                aid = workItem.Aid,
                WorkflowId = workItem.WorkflowId,
                //SeqNo = GetMaxSeq(workItem.WorkflowId) + 1,
                FromState = workItem.Fromstate,
                Trigger = workItem.Trigger,
                DataType = workItem.Datatype,
                Data = workItem.Data != null ? JsonConvert.DeserializeObject(workItem.Data) : null,
                Description = workItem.Description,
                AssignedRole = workItem.Assignedrole,
                AssignedUser = workItem.Assigneduser
            })
                .ToList();
        }

        public WorkItemResponse GetLastWorkItem(Guid workflowId)
        {
            var workflow = GetWorkflow(workflowId);

            var workItem = Context.WorkItem.Where(wi => wi.WorkflowId == workflowId)
                .OrderBy(wi => wi.Seqno).LastOrDefault();

            if (workItem == null) return null;

            return new WorkItemResponse
            {
                Id = workItem.Id,
                WorkflowId = workItem.WorkflowId,
                SeqNo = GetMaxSeq(workItem.WorkflowId) + 1,
                FromState = workItem.Fromstate,
                Trigger = workItem.Trigger,
                DataType = workItem.Datatype,
                Data = workItem.Data != null ? JsonConvert.DeserializeObject(workItem.Data) : null,
                Description = workItem.Description,
                AssignedRole = workItem.Assignedrole,
                AssignedUser = workItem.Assigneduser,
                Observer = workflow.Observer,
                parentWFID = workflow.parentWFID,
                CurrentState = workflow.CurrentState
                
            };
        }


        public int GetWorkflowState(Guid workItemId)
        {
            return Context.Workflow.First(wf => wf.Id == workItemId).Currentstate;
        }

        public Guid GetWorkflowId(Guid workItemId)
        {
            return Context.WorkItem.First(wi => wi.Id == workItemId).WorkflowId;
        }


        private int GetMaxSeq(Guid id)
        {
            var seqs = Context.WorkItem.Where(wi => wi.WorkflowId == id);

            return !seqs.Any() ? 0 : seqs.Select(wi => wi.Seqno).Max();
        }

        public Type GetWorkflowModelType(WorkflowTypes type)
        {
            Dictionary<WorkflowTypes, Type> typeModels = new Dictionary<WorkflowTypes, Type>()
            {
           

            };

            return typeModels[type];
        }



        public dynamic GetWorkflowViewModel(Guid WorkflowId)
        {


            var s = GetWorkflow(WorkflowId);
            Type type = GetWorkflowModelType((WorkflowTypes)s.TypeId);

            dynamic Model = Activator.CreateInstance(typeof(WorkflowViewModel<>).MakeGenericType(new Type[] { type }));

            Model.Workflow = s;
            Model.Workitem = GetLastWorkItem(WorkflowId);
            var docs = Context.WorkflowDocuments.Where(m => m.Workflowid == WorkflowId);
            Model.WorkflowDocuments = docs.Count() > 0 ? docs.ToList() : null;
            Model.Data = Model.Workitem.Data;
            return Model;
        }

        


        //public WorkflowResubmissionModel GetWorkflowResubmissionModel(Guid WorkflowId)
        //{
        //    WorkflowResubmissionModel Model = new WorkflowResubmissionModel();
        //    var s = GetWorkflow(WorkflowId);
        //    Type type = GetWorkflowModelType((WorkflowTypes)s.TypeId);
        //    dynamic WorkitemGeneric = Activator.CreateInstance(typeof(WorkItemResponsGeneric<>).MakeGenericType(new Type[] { type }));
        //    var workitem = GetLastWorkItem(WorkflowId);
        //    WorkitemGeneric.Data = workitem.Data;
        //    var properties = Model.GetType().GetProperties();
        //    foreach (var item in properties)
        //    {
        //        if (item.PropertyType == type)
        //        {
        //            item.SetValue(Model, WorkitemGeneric.GetData);
        //        }
        //    }
        //    Model.Workflow = s;
        //    Model.Workitem = workitem;
        //    var docs = Context.WorkflowDocuments.Where(m => m.WorkflowId == WorkflowId);
        //    Model.WorkflowDocuments = docs.Count() > 0 ? docs.ToList() : null;
        //    return Model;
        //}

        public WorkflowDocuments GetWorkflowDocument(int id)
        {
            return Context.WorkflowDocuments.Find(id);
        }

        public List<WorkflowDocuments> GetWorkflowDocuments(Guid WorkflowId)
        {
            return Context.WorkflowDocuments.Where(m => m.Workflowid == WorkflowId).ToList();
        }

        public void DeleteWorkflowDocument(int id)
        {
            var doc = GetWorkflowDocument(id);
            Context.WorkflowDocuments.Remove(doc);
            Context.SaveChanges();
        }

       


    }


}
