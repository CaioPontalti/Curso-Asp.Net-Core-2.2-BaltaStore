using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Domain.StoreContext.Interfaces.Repositories;
using BaltaStore.Domain.StoreContext.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Tests.Mocks
{
    public class MockCustomerRepository : ICustomerRepository
    {
        public bool CheckDocumentExists(string document)
        {
            return false;
        }

        public bool CheckEmailExists(string email)
        {
            return false;
        }

        public void Save(Customer customer)
        {
            
        }


        //Consulta da Query para buscar a quantida de pedido do cliente.
        public CustomerOrderCountResult GetOrdersCustomerCount(string document)
        {
            return new CustomerOrderCountResult();
        }

        public IEnumerable<ListCustomerResult> Get()
        {
            throw new NotImplementedException();
        }

        public CustomerResult Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListOrdersCustomerResult> GetOrders(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
