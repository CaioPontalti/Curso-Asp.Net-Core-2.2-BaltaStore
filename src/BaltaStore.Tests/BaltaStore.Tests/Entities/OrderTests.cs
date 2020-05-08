using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Domain.StoreContext.Enums;
using BaltaStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private Customer _customer;
        private Order _order;

        private Product _product1;
        private Product _product2;
        private Product _product3;
        private Product _product4;


        public OrderTests()
        {
            var name = new Name("Caio", "Pontalti");
            var document = new Document("37032555870");
            var email = new Email("caio@gmail.com");
            _customer = new Customer(name, document, email, "996347734");
            _order = new Order(_customer);

            _product1 = new Product("Titulo Produto 1", "Descrição Produto 1", "ima1.jpg", 100.0M, 10);
            _product2 = new Product("Titulo Produto 2", "Descrição Produto 2", "ima2.jpg", 100.0M, 10);
            _product3 = new Product("Titulo Produto 3", "Descrição Produto 3", "ima3.jpg", 100.0M, 10);
            _product4 = new Product("Titulo Produto 4", "Descrição Produto 4", "ima4.jpg", 100.0M, 10);

        }

        [TestMethod]
        public void DeveCriarUmPedidoValido()
        {
            Assert.AreEqual(true, _order.IsValid);
        }

        [TestMethod]
        public void AoCriarUmPedidoStatusDeveSerCreated()
        {
            Assert.AreEqual(EOrderStatus.Created, _order.Status);
        }

        [TestMethod]
        public void AoAdicionarUmNovoItemQuantidadeDoPedidoDeveMudar()
        {
            _order.AddItem(_product1, 2);
            _order.AddItem(_product2, 5);

            Assert.AreEqual(2, _order.Items.Count);
        }

        [TestMethod]
        public void AoAdicionarUmNovoItemDeveSubtrairQuantidadeEstoque()
        {
            _order.AddItem(_product1, 5);

            Assert.AreEqual(_product1.QuantityOnHand, 5);
        }

        [TestMethod]
        public void AoConfirmarPedidoDeveGerarNumero()
        {
            _order.Place();

            Assert.AreNotEqual("", _order.Number);
        }

        [TestMethod]
        public void AoPagarUmPedidoStatusDeveSerPago()
        {
            _order.Pay();

            Assert.AreEqual(EOrderStatus.Paid, _order.Status);
        }

        [TestMethod]
        public void AoTerUmPedidoComDezProdutosGerarDuasEntregas()
        {
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.Ship();

            Assert.AreEqual(2, _order.Deliveries.Count);
        }

        [TestMethod]
        public void AoCancelarOPedidoOStatusDeveSerCancelado()
        {
            _order.Cancel();

            Assert.AreEqual(EOrderStatus.Canceled, _order.Status);
        }

        [TestMethod]
        public void AoCancelarOPedidoCancelarAsEntregas()
        {
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            _order.AddItem(_product1, 1);
            
            _order.Ship();

            _order.Cancel();

            foreach (var item in _order.Deliveries)
            {
                Assert.AreEqual(EDeliveryStatus.Canceled, item.Status);
            }
        }
    }
}
