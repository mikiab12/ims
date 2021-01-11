using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ims.domain.Workflows
{
    public enum WorkflowTypes
    {
        FactoryToStore = 1,
        StoreToShop = 2,
        PurchaseToStore = 3,
        ShopToStore = 4
    }
}
