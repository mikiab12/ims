using ims.domain.Admin;
using ims.domain.Infrastructure;
using ims.domain.Transaction.Models;
using ims.domain.Workflows;
using Stateless;
using System;
using System.Collections.Generic;
using System.Text;

namespace ims.domain.Transaction.StateMachines
{
    public class FactoryToStoreWorkflow : BaseWorkflow<FactoryToStoreModel>
    {
        public FactoryToStoreWorkflow()
        {
            base.type = WorkflowTypes.FactoryToStore;
            base.approverRole = (long)UserRoles.StoreOfficer;
            base.initiatorRole = (long)UserRoles.FactoryOfficer;
        }

        public FactoryToStoreWorkflow(UserSession session) : base(session)
        {
            base.type = WorkflowTypes.FactoryToStore;
            base.approverRole = (long)UserRoles.StoreOfficer;
        }
    }
}
