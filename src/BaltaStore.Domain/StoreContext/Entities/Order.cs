using BaltaStore.Domain.StoreContext.Enums;
using BaltaStore.Shared.Entities;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaltaStore.Domain.StoreContext.Entities
{
    public class Order : Entity
    {
        private readonly IList<OrderItem> _orderItems;
        private readonly IList<Delivery> _deliveries;

        public Order(Customer customer)
        {
            Customer = customer;
            CreateDate = DateTime.Now;
            Status = EOrderStatus.Created;
            _orderItems = new List<OrderItem>();
            _deliveries = new List<Delivery>();
        }

        public Customer Customer { get; private set; }
        public string Number { get; private set; }
        public DateTime CreateDate { get; private set; }
        public EOrderStatus Status { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _orderItems.ToArray();
        public IReadOnlyCollection<Delivery> Deliveries => _deliveries.ToArray();

        public void AddItem(Product product, decimal quantity)
        {
            if (product.QuantityOnHand < quantity)
                AddNotification("Quantidade", "Quantidade comprada maior que o disponivel");

            var item = new OrderItem(product, quantity);
            _orderItems.Add(item);
        }

        public void AddDelivery(Delivery delivery)
        {
            _deliveries.Add(delivery);
        }

        public void Place()
        {
            //Gera o número do pedido
            Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            if (_orderItems.Count == 0)
                AddNotification("Pedido", "O Pedido não possui itens");
        }

        public void Pay()
        {
            Status = EOrderStatus.Paid;
        }

        public void Ship()
        {
            // A cada 5 produtos, gera nova entrega
            var deliveries = new List<Delivery>();
            var count = 1;

            foreach (var item in _orderItems)
            {
                if (count == 5)
                {
                    count = 1;
                    deliveries.Add(new Delivery(DateTime.Now.AddDays(5)));
                }

                count++;
            }

            //Envia todas as entregas
            foreach (var item in deliveries)
            {
                item.Ship();
            }

            //Adiciona as entregas no pedido
            foreach (var item in deliveries)
            {
                _deliveries.Add(item);
            }
        }

        public void Cancel()
        {
            Status = EOrderStatus.Canceled;
            foreach (var item in _deliveries)
            {
                item.Cancel();
            }
        }
    }
}
