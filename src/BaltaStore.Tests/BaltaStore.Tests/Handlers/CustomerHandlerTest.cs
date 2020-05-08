using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using BaltaStore.Domain.StoreContext.Handlers;
using BaltaStore.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Shared.Handlers
{
    [TestClass]
    public class CustomerHandlerTest
    {
        [TestMethod]
        public void DeveRetornarUmCustomerCommandQuandoForValido()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Caio";
            command.LastName = "Pontalti";
            command.Document = "37032555870";
            command.Email = "caio@gmail.com";
            command.Phone = "11996347737";

            var handler = new CustomerHandler(new MockCustomerRepository(), new MockEmailService());
            var result = handler.Handle(command);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(true, handler.IsValid);
        }
    }
}
