using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Shared.Commands;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Domain.StoreContext.Commands.OrderCommands
{
    public class CreateOrderCommand : Notifiable,  ICommand
    {
        public CreateOrderCommand()
        {
            OrderItems = new List<CreateOrderItemCommand>();

        }
        public Guid CustomerId { get; set; }
        public List<CreateOrderItemCommand> OrderItems { get; set; }

        public bool Valid()
        {
            AddNotifications(new ValidationContract()
                .HasLen(CustomerId.ToString(), 36, "Customer", "Identificador do Cliente inválido")
                .IsGreaterThan(OrderItems.Count, 0, "Items", "O pedido não possui itens.")
            );
            return IsValid;
        }
    }
}
