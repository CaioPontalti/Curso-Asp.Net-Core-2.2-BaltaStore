using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Domain.StoreContext.Queries
{
    public class ListOrdersCustomerResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Order { get; set; }
        public decimal Total { get; set; }
    }
}
