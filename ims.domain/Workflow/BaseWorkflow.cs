using ims.data;
using ims.data.Entities;
using ims.domain.Admin;
using ims.domain.Infrastructure;
using ims.domain.Infrastructure.Architecture;
using ims.domain.Workflows;
using ims.domain.Workflows.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ims.domain.Workflows
{
    public class BaseWorkflow<T> : IMSService where T : class
    {

        public WorkflowTypes type;
        public long approverRole;
        public long initiatorRole;

        public BaseWorkflow(WorkflowTypes s)
        {
            this.type = s;
        }

        public enum States
        {
            Filing = 0,
            Cancelled = -1,
            Reviewing = 1,
            Approved = -2
        }

        public enum Triggers
        {
            Cancel = 1,
            Request = 2,
            Reject = 3,
            Approve = 4
        }

        //public IBDLService _service;


        private readonly IWorkflowService _workflowService;
        public readonly IWorkflowDocumentsService<T> _wfDocument;

        private StateMachine<States, Triggers> _machine;

        public BaseWorkflow(UserSession session)
        {

            _workflowService = new WorkflowService();
            _workflowService.SetSession(session);
            _wfDocument = new WorkflowDocumentsService<T>();
            _wfDocument.SetSession(session);
        }

        public BaseWorkflow()
        {
            _workflowService = new WorkflowService();
            _wfDocument = new WorkflowDocumentsService<T>();
        }

        public void SetSession(UserSession session)
        {
          
            _workflowService.SetSession(session);
            _wfDocument.SetSession(session);
        }

        public Workflow Workflow { get; set; }

        public override void SetContext(StoreDbContext value)
        {
            Context = value;
            _workflowService.SetContext(Context);
            _wfDocument.SetContext(Context);
        }

        public void ConfigureMachine(string description, int? EmpID)
        {
            Workflow = _workflowService.CreateWorkflow(new WorkflowRequest
            {
                CurrentState = (int)States.Filing,
                Description = description,
                TypeId = (int)type,
                EmployeeID = EmpID

            });
            _machine = new StateMachine<States, Triggers>(States.Filing);
            DefineStateMachines();
        }

        public void ConfigureMachine(Guid workflowId)
        {
            Workflow = Context.Workflow.First(wf => wf.Id == workflowId && wf.Typeid == (int)type);
            _machine = new StateMachine<States, Triggers>((States)Workflow.Currentstate);
            DefineStateMachines();
        }

        public void DefineStateMachines()
        {
            ParameterizedTriggers.ConfigureParameters(_machine);

            _machine.Configure(States.Filing)

                .OnEntryFrom(ParameterizedTriggers.Reject, OnReject)
                .Permit(Triggers.Cancel, States.Cancelled)
                .Permit(Triggers.Request, States.Reviewing);

            _machine.Configure(States.Cancelled)
                .OnEntryFrom(ParameterizedTriggers.Cancel, OnCancel);

            _machine.Configure(States.Reviewing)
                .OnEntryFrom(ParameterizedTriggers.Request, OnRequest)
                .Permit(Triggers.Reject, States.Filing)
                .Permit(Triggers.Approve, States.Approved);

            _machine.Configure(States.Approved)
                .OnEntryFrom(ParameterizedTriggers.Approve, OnApprove);
        }

        public void Fire(Guid workflowId, StateMachine<States, Triggers>.TriggerWithParameters<string, long?> trigger,
            string description, long? assignedUser)
        {

            _machine.Fire(trigger, description, assignedUser);
            _workflowService.UpdateWorkflow(workflowId, (int)_machine.State, description);
        }

        public void Fire(Guid workflowId, StateMachine<States, Triggers>.TriggerWithParameters<T, string, long?> trigger,
            T data, string description, long? assignedUser)
        {
            _machine.Fire(trigger, data, description, assignedUser);
            _workflowService.UpdateWorkflow(workflowId, (int)_machine.State, description);

        }

        public void OnCancel(string description, long? assignedUser, StateMachine<States, Triggers>.Transition transition)
        {
            ConfigureAndAddWorkItem(null, GetData(), description, assignedUser, transition);
            _wfDocument.DeleteFiles(Workflow.Id);
        }


        public void OnRequest(T data, string description, long? assignedUser, StateMachine<States, Triggers>.Transition transition)
        {
            ConfigureAndAddWorkItem(approverRole, data, description, assignedUser, transition);
        }

        public void OnApprove(T data, string description, long? assignedUser, StateMachine<States, Triggers>.Transition transition)
        {
            ConfigureAndAddWorkItem(null, data, description, assignedUser, transition);
        }

        public void OnReject(string description, long? assignedUser, StateMachine<States, Triggers>.Transition transition)
        {
            var initialUser = Context.Workflow.Find(Workflow.Id).Initiatoruser;
            var user = Context.User.Where(m => m.Username == initialUser).First();
            ConfigureAndAddWorkItem(initiatorRole, GetData(), description, null, transition);
        }

        public T GetData()
        {
            var workItem = Context.WorkItem.Where(w => w.WorkflowId == Workflow.Id).OrderBy(wi => wi.Seqno).
                LastOrDefault();
            return workItem != null ? JsonConvert.DeserializeObject<T>(workItem.Data, new JsonSerializerSettings()
            {
                ContractResolver = new IgnoreIFormFileResolver()
            }) : null;
        }

        public void ConfigureAndAddWorkItem(long? role, T data, string description, long? assignedUser,
            StateMachine<States, Triggers>.Transition transition)
        {
            _workflowService.CreateWorkItem(new WorkItemRequest
            {
                WorkflowId = Workflow.Id.ToString(),
                FromState = (int)transition.Source,
                Trigger = (int)transition.Trigger,
                DataType = typeof(T).ToString(),
                Data = data != null ? JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings()
                {
                    ContractResolver = new IgnoreIFormFileResolver()
                }) : null,
                Description = description,
                AssignedRole = role,
                AssignedUser = assignedUser
            });
        }



        public static class ParameterizedTriggers
        {
            public static StateMachine<States, Triggers>.TriggerWithParameters<string, long?> Cancel;
            public static StateMachine<States, Triggers>.TriggerWithParameters<T, string, long?> Request;
            public static StateMachine<States, Triggers>.TriggerWithParameters<string, long?> Reject;
            public static StateMachine<States, Triggers>.TriggerWithParameters<T,string, long?> Approve;

            public static void ConfigureParameters(StateMachine<States, Triggers> machine)
            {
                Cancel = machine.SetTriggerParameters<string, long?>(Triggers.Cancel);
                Request = machine.SetTriggerParameters<T, string, long?>(Triggers.Request);
                Reject = machine.SetTriggerParameters<string, long?>(Triggers.Reject);
                Approve = machine.SetTriggerParameters<T,string, long?>(Triggers.Approve);
            }
        }
    }

    public class IgnoreIFormFileResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            if (prop.PropertyType == typeof(IFormFile) || prop.PropertyType == typeof(IList<IFormFile>))
            {
                prop.Ignored = true;
            }
            return prop;
        }
    }
}
