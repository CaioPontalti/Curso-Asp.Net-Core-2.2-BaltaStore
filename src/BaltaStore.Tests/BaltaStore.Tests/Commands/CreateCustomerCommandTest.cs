using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BaltaStore.Tests.Commands
{
    [TestClass]
    public class CreateCustomerCommandTest
    {
        [TestMethod]
        public void RetornarValidoQuandoUmComandoForValido()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Caio";
            command.LastName = "Pontalti";
            command.Document = "37032555870";
            command.Email = "caio@gmail.com";
            command.Phone = "11996347737";

            Assert.AreEqual(true, command.Valid());
        }
    }
}
