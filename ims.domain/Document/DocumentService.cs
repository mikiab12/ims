using ims.domain.Infrastructure;
using ims.domain.Infrastructure.Architecture;
using System;
using System.Collections.Generic;
using System.Text;
using ims.data.Entities;
using ims.data;
using Microsoft.Extensions.Caching.Memory;
using ims.domain.Workflows;
using ims.domain.Admin;
using System.Linq;

namespace ims.domain.Documents
{
    public interface IDocumentService : iIMSService
    {
        void SetSession(UserSession session);
        void SetSessionConfiguration(IMSConfiguration Config, UserSession session);
        data.Entities.Document AddDocument(data.Entities.Document doc);
        //Document GetDocument(Guid wfid);
        data.Entities.Document GetDocument(int id);
        void RemoveDocument(int DocID);
    }

    public class DocumentService : IMSService, IDocumentService
    {


        private StoreDbContext _context;
        private UserSession _session;
        private string _url;
        private string _sessionID;
        private IMemoryCache _cache;
        private IMSConfiguration _config;

        private IWorkflowService _wfService;
        private IUserActionService _userActionService;


        public DocumentService(StoreDbContext Context, IMemoryCache memoryCache, IWorkflowService wfService, IUserActionService actionSercice)
        {
            _context = Context;
            _cache = memoryCache;
            _wfService = wfService;
            _wfService.SetContext(Context);
            _userActionService = actionSercice;

        }
        public void SetSession(UserSession session)
        {
            _session = session;
            _sessionID = session.payrollSessionID;
            _wfService.SetSession(session);
        }

        public void SetSessionConfiguration(IMSConfiguration Config, UserSession session)
        {
            _sessionID = session.payrollSessionID;
            _session = session;
          //  _url = Config.Host;
            _config = Config;
        }

        public data.Entities.Document AddDocument(data.Entities.Document doc)
        {
            _context.Documents.Add(doc);
            _context.SaveChanges(_session.Username, (int)UserActionType.AddDocument);
            return doc;
        }

        //public data.Entities.Document GetDocument(Guid wfid)
        //{
        //    var doc = _context.Documents.Where(m => m.WorkflowId == wfid).FirstOrDefault();
        //    return doc;
        //}

        public data.Entities.Document GetDocument(int id)
        {
            var doc = _context.Documents.Find(id);
            return doc;
        }

        public void RemoveDocument(int DocID)
        {
            _context.Documents.Remove(_context.Documents.Find(DocID));
            _context.SaveChanges(_session.Username, (int)UserActionType.DeleteDocument);
        }
    }
}
