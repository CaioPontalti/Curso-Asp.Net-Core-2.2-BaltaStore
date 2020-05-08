using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Domain.StoreContext.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Domain.StoreContext.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        bool CheckDocumentExists(string document);

        bool CheckEmailExists(string email);

        void Save(Customer customer);

        CustomerOrderCountResult GetOrdersCustomerCount(string document);

        IEnumerable<ListCustomerResult> Get();

        CustomerResult Get(Guid id);

        IEnumerable<ListOrdersCustomerResult> GetOrders(Guid id);
    }
}
