using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Domain.StoreContext.Handlers;
using BaltaStore.Domain.StoreContext.Interfaces.Repositories;
using BaltaStore.Domain.StoreContext.Queries;
using BaltaStore.Shared.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaltaStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerHandler _handler;

        public CustomerController(ICustomerRepository customerRepository, CustomerHandler handler)
        {
            _customerRepository = customerRepository;
            _handler = handler;
        }


        //Versionamento na rota
        [HttpGet]
        [Route("v1/customers")]
        public IEnumerable<ListCustomerResult> Get()
        {
              return _customerRepository.Get();
        }

        //Versionamento na rota
        [HttpGet]
        [Route("v2/customers")]
        [ResponseCache(Duration = 60)] //Cache de 60 minutos
        public IEnumerable<ListCustomerResult> GetNew()
        {
            return _customerRepository.Get();
        }


        [HttpGet]
        [Route("customers/{id}")]
        public CustomerResult GetById(Guid id)
        {
            return _customerRepository.Get(id);
        }

        [HttpGet]
        [Route("customers/{id}/orders")]
        public IEnumerable<ListOrdersCustomerResult> GetOrders(Guid id)
        {
            return _customerRepository.GetOrders(id);
        }

        [HttpPost]
        [Route("customers")]
        public ICommandResult Post([FromBody] CreateCustomerCommand command) 
        {
            var result = _handler.Handle(command);

            return result;
        }


        //NÃO IMPLEMENTADOS
        [HttpPut]
        [Route("customers/{id}")]
        public ICommandResult Put(Guid id, [FromBody] CreateCustomerCommand command)
        {
            return null;
        }

        [HttpDelete]
        [Route("customers/{id}")]
        public ICommandResult Delete(Guid id)
        {
            return null;
        }

    }
}