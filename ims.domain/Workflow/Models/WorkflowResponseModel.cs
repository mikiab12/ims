using ims.data.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ims.domain.Workflows.Models
{
    public class WorkflowResponse
    {
        public Guid Id { get; set; }
        public int CurrentState { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public long? Aid { get; set; }
        public int? EmployeeID { get; set; }
        public string Observer { get; set; }
        public System.Nullable<Guid> parentWFID { get; set; }

        public WorkflowTypeResponse Type;
        public WorkflowActionResponse Action;
    }

    public class WorkflowTypeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class WorkflowActionResponse
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string TimeStr { get; set; }
    }

    public class WorkItemResponse
    {

        public Guid Id { get; set; }
        public Guid WorkflowId { get; set; }
        public string Observer { get; set; }
        public System.Nullable<Guid> parentWFID { get; set; }
        public int SeqNo { get; set; }
        public int FromState { get; set; }
        public int Trigger { get; set; }
        public int CurrentState { get; set; }
        public string DataType { get; set; }
        public object Data { get; set; }
        public string Description { get; set; }
        public long? AssignedRole { get; set; }
        public long? AssignedUser { get; set; }
        public long? aid { get; set; }


    }

    public class WorkItemResponsGeneric<T> : WorkItemResponse where T : class
    {
        public T GetData
        {
            get
            {
                return JsonConvert.DeserializeObject<T>(Data.ToString(), new JsonSerializerSettings()
                {
                    ContractResolver = new IgnoreIFormFileResolver()
                });

            }
        }
    }
    public class WorkflowViewModelBase
    {
        public WorkflowResponse Workflow { get; set; }
        public WorkItemResponse Workitem { get; set; }
        public object Data { get; set; }
        public IList<WorkflowDocuments> WorkflowDocuments { get; set; }
        public List<Document> Documents { get; set; }
    }

    public class WorkflowViewModel<T> : WorkflowViewModelBase where T : class
    {

        public T GetData
        {
            get
            {
                return JsonConvert.DeserializeObject<T>(Workitem.Data.ToString(), new JsonSerializerSettings()
                {
                    ContractResolver = new IgnoreIFormFileResolver()
                });

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
            if(prop.PropertyType == typeof(byte[]))
            {
                prop.Ignored = true;
            }
            return prop;
        }
    }


    public enum OperationType
    {
        New = 0,
        Update = 1
    }

 

}

