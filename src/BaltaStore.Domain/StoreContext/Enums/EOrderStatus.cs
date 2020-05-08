using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Domain.StoreContext.Enums
{
    public enum EOrderStatus
    {
        Created = 1,
        Paid = 2,
        Shipped = 3,
        Canceled = 4
    }
}
