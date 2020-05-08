using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Domain.StoreContext.Interfaces.Repositories;
using BaltaStore.Domain.StoreContext.Queries;
using BaltaStore.Infra.DataContext;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace BaltaStore.Infra.Repositories
{
    public class CustormerRepository : ICustomerRepository
    {
        private readonly StoreDataContext _context;
        public CustormerRepository(StoreDataContext context)
        {
            _context = context;
        }

        public bool CheckDocumentExists(string document)
        {
            using (var sqlConn = _context.Connection)
            {
                var result = sqlConn.Query<bool>("spCheckDocument",
                    new { @Document = document },
                    commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();

                return result;
            }
        }

        public bool CheckEmailExists(string email)
        {
            using (var sqlConn = _context.Connection)
            {
                var result = sqlConn.Query<bool>("spCheckEmail",
                    new { @Email = email },
                    commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();
                
                return result;
            }
        }

        public IEnumerable<ListCustomerResult> Get()
        {
            using (var sqlConn = _context.Connection)
            {
                var result = sqlConn.Query<ListCustomerResult>(
                    "SELECT [Id], [FirstName], [Document], [Email] FROM [CUSTOMER] ",
                    null
                    );

                return result;
            }
        }

        public CustomerResult Get(Guid id)
        {
            using (var sqlConn = _context.Connection)
            {
                var result = sqlConn.Query<CustomerResult>(
                    "SELECT [Id], [FirstName], [Document], [Email] FROM [CUSTOMER] WHERE id=@ID",
                    new { @ID = id}
                    ).FirstOrDefault();

                return result;
            }
        }

        public IEnumerable<ListOrdersCustomerResult> GetOrders(Guid id)
        {
            using (var sqlConn = _context.Connection)
            {
                var result = sqlConn.Query<ListOrdersCustomerResult>("MinhaQueryOuProcedure",
                    new { @ID = id });

                return result;
            }
        }

        public CustomerOrderCountResult GetOrdersCustomerCount(string document)
        {
            using (var sqlConn = _context.Connection)
            {
                var result = sqlConn.Query<CustomerOrderCountResult>("MinhaQueryOuProcedure",
                    new { @Document = document},
                    commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();

                return result;
            }
        }

        public void Save(Customer customer)
        {
            using (var sqlConn = _context.Connection)
            {
                sqlConn.Execute("spCreateCustomer",
                    new
                    {
                        @Id = customer.Id,
                        @FirstName = customer.Name.FirstName,
                        @LastName = customer.Name.LastName,
                        @Document = customer.Document,
                        @Email = customer.Email,
                        @Phone = customer.Phone
                    },
                    commandType:CommandType.StoredProcedure
                );

                foreach (var adrress in customer.Addresses)
                {
                    sqlConn.Execute("spCreateAddress",
                        new
                        {
                            @Id = adrress.Id,
                            @CustomerId = customer.Id,
                            @Number = adrress.Number,
                            @Complement = adrress.Complement,
                            @District = adrress.District,
                            @City = adrress.City,
                            @State = adrress.State,
                            @Country = adrress.Country,
                            @ZipCode = adrress.ZipCode,
                            @Type = adrress.Type
                        },
                        commandType: CommandType.StoredProcedure);
                }
            }
        }
    }
}
