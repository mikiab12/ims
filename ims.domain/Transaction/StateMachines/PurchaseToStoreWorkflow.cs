using ims.domain.Admin;
using ims.domain.Infrastructure;
using ims.domain.Transaction.Models;
using ims.domain.Workflows;

namespace ims.domain.Transaction.StateMachines
{
    public class PurchaseToStoreWorkflow : BaseWorkflow<PurchaseToStoreModel>
    {
        public PurchaseToStoreWorkflow()
        {
            base.type = WorkflowTypes.PurchaseToStore;
            base.approverRole = (long)UserRoles.StoreOfficer;
            base.initiatorRole = (long)UserRoles.Purchaseofficer;
        }

        public PurchaseToStoreWorkflow(UserSession session) : base(session)
        {
            base.type = WorkflowTypes.PurchaseToStore;
            base.approverRole = (long)UserRoles.StoreOfficer;
        }


    }
}
