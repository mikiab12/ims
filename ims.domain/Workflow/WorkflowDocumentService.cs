using ims.data.Entities;
using ims.domain.Infrastructure;
using ims.domain.Infrastructure.Architecture;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ims.domain.Workflows
{
    public interface IWorkflowDocumentsService<T> : iIMSService where T : class
    {
        void SetSession(UserSession session);
        void CreateWorkflowDocument(Guid WorkflowId, IFormFile file, int? index);
        void ExtractFileFromModel(T Model, Guid WorkflowId, int? index);
        WorkflowDocuments GetFormFile(Guid WorkflowId, string PropertyName);
        List<WorkflowDocuments> GetFormFiles(Guid WorkflowId, string PropertyName);
        T ReconstructFileDocs(T Model, Guid WorkflowId);
        void DeleteFiles(Guid WorkflowId);
        void UploadFile(Guid WorkflowId, IFormFile file);



    }


    public class WorkflowDocumentsService<T> : IMSService, IWorkflowDocumentsService<T> where T : class
    {
        private UserSession _session;

        public void SetSession(UserSession session)
        {
            _session = session;
        }



        public void CreateWorkflowDocument(Guid WorkflowId, IFormFile file, int? index)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var wf = Context.Workflow.Find(WorkflowId);
                string name;
                if (wf.Typeid == 29 || wf.Typeid == 30)
                    name = file.Name;
                else
                    name = file.Name.Split('.').Count() > 1 ? file.Name.Split('.')[1] : file.Name;
                WorkflowDocuments doc = new WorkflowDocuments
                {
                    Workflowid = WorkflowId,
                    Filename = "_" + file.FileName,
                    Contentdisposition = file.ContentDisposition,
                    Contenttype = file.ContentType,
                    Name = name,
                    Length = file.Length,
                    File = ms.ToArray(),
                    Index = index
                };
                Context.WorkflowDocuments.Add(doc);
                Context.SaveChanges();
                // return doc;
            }

            //   UploadFile(WorkflowId, file);

        }

        public WorkflowDocuments GetFormFile(Guid WorkflowId, string PropertyName)
        {
            var doc = Context.WorkflowDocuments.Where(m => m.Workflowid == WorkflowId).LastOrDefault();
            return doc;
        }

        public List<WorkflowDocuments> GetFormFiles(Guid WorkflowId, string PropertyName)
        {
            var docs = Context.WorkflowDocuments.Where(m => m.Workflowid == WorkflowId).ToList();
            return docs;
        }

        public void ExtractFileFromModel(T Model, Guid WorkflowId, int? index)
        {
            PropertyInfo[] Props = Model.GetType().GetProperties();
            foreach (PropertyInfo info in Props)
            {
                if (info.PropertyType == typeof(IFormFile))
                {
                    var file = (IFormFile)info.GetValue(Model);
                    if (file != null)
                    {
                        CreateWorkflowDocument(WorkflowId, file, index);
                    }
                }
                else if (info.PropertyType == typeof(IList<IFormFile>))
                {
                    var files = (IList<IFormFile>)info.GetValue(Model);
                    if (files != null && files.Count > 0)
                    {
                        foreach (IFormFile file in files)
                        {
                            CreateWorkflowDocument(WorkflowId, file, index);

                        }
                    }
                }

            }
        }


        public T ReconstructFileDocs(T Model, Guid WorkflowId)
        {
            PropertyInfo[] Props = Model.GetType().GetProperties();
            foreach (PropertyInfo info in Props)
            {
                if (info.PropertyType == typeof(IFormFile))
                {
                    WorkflowDocuments doc = GetFormFile(WorkflowId, info.Name);
                    var wfInfo = Props.Where(m => m.Name == "wf" + info.Name).FirstOrDefault();
                    wfInfo.SetValue(Model, doc);
                    // info.SetValue(Model, GetFormFile(WorkflowId, info.Name));

                }
                else if (info.PropertyType == typeof(IList<IFormFile>) || info.PropertyType == typeof(List<IFormFile>))
                {
                    List<WorkflowDocuments> docs = GetFormFiles(WorkflowId, info.Name);
                    var wfInfo = Props.Where(m => m.Name == "wf" + info.Name && m.PropertyType == typeof(List<WorkflowDocuments>)).FirstOrDefault();
                    wfInfo.SetValue(Model, docs);
                    //info.SetValue(Model, GetFormFiles(WorkflowId, info.Name));
                }

            }
            return Model;
        }

        public void DeleteFiles(Guid WorkflowId)
        {
            var docs = Context.WorkflowDocuments.Where(m => m.Workflowid == WorkflowId).ToList();
            if (docs.Count > 0)
            {
                foreach (var item in docs)
                {
                    Context.WorkflowDocuments.Remove(item);
                }
                Context.SaveChanges();
            }
        }
        public async void UploadFile(Guid WorkflowId, IFormFile file)
        {
            var filename = "_" + file.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Workflow", filename);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
    }
}
