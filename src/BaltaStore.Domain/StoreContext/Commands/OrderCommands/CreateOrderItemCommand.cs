using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Domain.StoreContext.Commands.OrderCommands
{
    public class CreateOrderItemCommand
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}
