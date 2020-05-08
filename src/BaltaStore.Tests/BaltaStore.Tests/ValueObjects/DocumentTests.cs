using BaltaStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        private Document _docValido;
        private Document _docInvalido;

        public DocumentTests()
        {
            _docInvalido = new Document("1234567890");
            _docValido = new Document("37032555870");
        }

        [TestMethod]
        public void RetornarNotificacaoQuandoCPFInvalido()
        {
            //Pode ter mais de 1 Assert
            Assert.AreEqual(false, _docInvalido.IsValid);
            Assert.AreEqual(1, _docInvalido.Notifications.Count);
        }

        [TestMethod]
        public void NaoRetornarNotificacaoQuandoCPFValido()
        {
            //Pode ter mais de 1 Assert
            Assert.AreEqual(true, _docValido.IsValid);
            Assert.AreEqual(0, _docValido.Notifications.Count);
        }
    }
}
