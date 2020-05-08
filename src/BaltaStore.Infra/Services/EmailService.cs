using BaltaStore.Domain.StoreContext.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Infra.Services
{
    public class EmailService : IEmailServices
    {
        public void Send(string to, string from, string subject, string body)
        {
            
            throw new NotImplementedException();
        }
    }
}
