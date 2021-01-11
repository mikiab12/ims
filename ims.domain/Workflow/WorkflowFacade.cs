using ims.data.Entities;
using ims.data;
using ims.domain.Infrastructure;
using ims.domain.Infrastructure.Architecture;
using ims.domain.Workflows.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ims.domain.Workflows
{
    public interface IWorkflowFacade : IiMSFacade
    {
        void SetSession(UserSession session);

        void CreateWorkflow(WorkflowRequest request);
        void UpdateWorkflow(Guid id, int currentState, string description);
        WorkflowResponse GetWorkflow(Guid id);
        IList<WorkflowResponse> GetUserWorkflows();

        void CreateWorkItem(WorkItemRequest request);
        IList<WorkItemResponse> GetWorkItems(Guid workflowId);
        WorkItemResponse GetLastWorkItem(Guid workflowId);
        Type GetWorkflowModelType(WorkflowTypes type);
        object GetWorkflowViewModel(Guid WorkflowId);
        //WorkflowResubmissionModel GetWorkflowResubmissionModel(Guid WorkflowId);
        WorkflowDocuments GetWorkflowDocument(int id);
        List<WorkflowDocuments> GetWorkflowDocuments(Guid WorkflowId);
        void DeleteWorkflowDocument(int id);
        //WorkflowHistoryModel GetPaymentWorkflowHistory(Guid wfid);
        //Dictionary<string, string> GetPaymentRequestSigners(Guid wfid);
        //EmployeeInformationViewModel GetEmployeeInformationViewModel(Guid WorkflowId);
    }

    public class WorkflowFacade : IMSFacade, IWorkflowFacade
    {
        private readonly IWorkflowService _service;
       // private readonly IWorkflowHelper _wfHelper;
        private UserSession _session;
        private StoreDbContext _context;

        public WorkflowFacade(IWorkflowService service, StoreDbContext Context) : base(Context)
        {
            _service = service;
            _context = Context;
            _service.SetContext(Context);
            //_wfHelper = wfHelper;
            //_wfHelper.SetContext(Context);
        }


        public void SetSession(UserSession session)
        {
            _session = session;
            _service.SetSession(_session);
            //_wfHelper.SetSession(_session);
        }


        public void CreateWorkflow(WorkflowRequest request)
        {
            Transact(t =>
            {
                PassContext(_service, _context);
                _service.CreateWorkflow(request);
            });
        }

        public void UpdateWorkflow(Guid id, int currentState, string description)
        {
            Transact(t =>
            {
                PassContext(_service, _context);
                _service.UpdateWorkflow(id, currentState, description);
            });
        }

        public WorkflowResponse GetWorkflow(Guid id)
        {
            return Transact(t =>
            {
                PassContext(_service, _context);
                return _service.GetWorkflow(id);
            });
        }

        public IList<WorkflowResponse> GetUserWorkflows()
        {
            return Transact(t =>
            {
                PassContext(_service, _context);
                return _service.GetUserWorkflows(_session);
            });
        }


        public void CreateWorkItem(WorkItemRequest request)
        {
            Transact(t =>
            {
                PassContext(_service, _context);
                _service.CreateWorkItem(request);
            });
        }

        public IList<WorkItemResponse> GetWorkItems(Guid workflowId)
        {
            // return Transact(t =>
            //{
            PassContext(_service, _context);
            return _service.GetWorkItems(workflowId);
            //});
        }

        public WorkItemResponse GetLastWorkItem(Guid workflowId)
        {
            //return Transact(t =>
            //{
            PassContext(_service, _context);
            return _service.GetLastWorkItem(workflowId);
            //});
        }

        public Type GetWorkflowModelType(WorkflowTypes type)
        {
            return Transact(t =>
            {
                PassContext(_service, _context);
                return _service.GetWorkflowModelType(type);
            });
        }

        public object GetWorkflowViewModel(Guid WorkflowId)
        {
            return Transact(t =>
            {
                PassContext(_service, _context);
                return _service.GetWorkflowViewModel(WorkflowId);
            });
        }

        //public WorkflowResubmissionModel GetWorkflowResubmissionModel(Guid WorkflowId)
        //{
        //    return Transact(t =>
        //    {
        //        PassContext(_service, _context);
        //        return _service.GetWorkflowResubmissionModel(WorkflowId);
        //    });
        //}

        public WorkflowDocuments GetWorkflowDocument(int id)
        {
            return Transact(t =>
            {
                PassContext(_service, _context);
                return _service.GetWorkflowDocument(id);
            });
        }

        public List<WorkflowDocuments> GetWorkflowDocuments(Guid WorkflowId)
        {
            return Transact(t =>
            {
                PassContext(_service, _context);
                return _service.GetWorkflowDocuments(WorkflowId);
            });
        }

        public void DeleteWorkflowDocument(int id)
        {
            Transact(t =>
            {
                PassContext(_service, _context);
                _service.DeleteWorkflowDocument(id);
            });
        }


        //public EmployeeInformationViewModel GetEmployeeInformationViewModel(Guid WorkflowId)
        //{
        //    return Transact(t =>
        //    {
        //        PassContext(_service, _context);
        //        return _service.GetEmployeeInformationViewModel(WorkflowId);
        //    });
        //}
    }

}
