using ims.domain.Admin;
using ims.domain.Infrastructure;
using ims.domain.Transaction.Models;
using ims.domain.Workflows;

namespace ims.domain.Transaction.StateMachines
{
    public class StoreToShopWorkflow : BaseWorkflow<StoreToShopModel>
    {

        public StoreToShopWorkflow()
        {
            base.type = WorkflowTypes.StoreToShop;
            base.approverRole = (long)UserRoles.ShopOfficer;
            base.initiatorRole = (long)UserRoles.StoreOfficer;
        }

        public StoreToShopWorkflow(UserSession session) : base(session)
        {
            base.type = WorkflowTypes.StoreToShop;
            base.approverRole = (long)UserRoles.ShopOfficer;
        }
    }
}
