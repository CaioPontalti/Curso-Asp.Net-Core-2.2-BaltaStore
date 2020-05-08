using BaltaStore.Domain.StoreContext.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Tests.Mocks
{
    public class MockEmailService : IEmailServices
    {
        public void Send(string to, string from, string subject, string body)
        {
            
        }
    }
}
