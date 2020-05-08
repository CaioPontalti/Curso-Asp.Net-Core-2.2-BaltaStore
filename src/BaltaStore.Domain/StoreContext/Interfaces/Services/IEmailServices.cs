using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Domain.StoreContext.Interfaces.Services
{
    public interface IEmailServices
    {
        void Send(string to, string from, string subject, string body);
    }
}
