using ims.domain.Admin;
using ims.domain.Infrastructure;
using ims.domain.Transaction.Models;
using ims.domain.Workflows;

namespace ims.domain.Transaction.StateMachines
{
    public class ShopToStoreWorkflow : BaseWorkflow<ShopToStoreModel>
    {
        public ShopToStoreWorkflow()
        {
            base.type = WorkflowTypes.ShopToStore;
            base.approverRole = (long)UserRoles.StoreOfficer;
            base.initiatorRole = (long)UserRoles.ShopOfficer;
        }

        public ShopToStoreWorkflow(UserSession session) : base(session)
        {
            base.type = WorkflowTypes.ShopToStore;
            base.approverRole = (long)UserRoles.StoreOfficer;
        }
    }
}
