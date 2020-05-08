using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Domain.StoreContext.Interfaces.Repositories;
using BaltaStore.Domain.StoreContext.Interfaces.Services;
using BaltaStore.Domain.StoreContext.ValueObjects;
using BaltaStore.Shared.Commands;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace BaltaStore.Domain.StoreContext.Handlers
{
    public class CustomerHandler : Notifiable, ICommandHandle<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailServices _emailServices;

        public CustomerHandler(ICustomerRepository customerRepository, IEmailServices emailServices)
        {
            _customerRepository = customerRepository;
            _emailServices = emailServices;
        }

        public ICommandResult Handle(CreateCustomerCommand command)
        {
            //Verificar se o CPF já existe na base
            if (_customerRepository.CheckDocumentExists(command.Document))
                AddNotification("Document", "Esse CPF já está cadastrado.");

            //Verificar se o E-mail já existe na base
            if (_customerRepository.CheckEmailExists(command.Email))
                AddNotification("Email", "Esse E-mail já está em uso.");

            //Criar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);

            //Criar a entidade
            var customer = new Customer(name, document, email, command.Phone);

            //Validar Entidade e VOs
            AddNotifications(name.Notifications);
            AddNotifications(document.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(customer.Notifications);

            if (Invalid) // Propriedade do Flunt (Se tiver notificações é inválido)
                return new ResponseCommand(false, "Verifique as Notificações", Notifications);

            //Persistir o Cliente
            _customerRepository.Save(customer);

            //Enviar um E-mail de boas vindas.
            _emailServices.Send(email.Address, "store@gmail.com", "Seja em vindo", "Corpo do email");


            //Retornar o resultado para tela
            return new ResponseCommand(
                true, //Success
                "Cliente cadastrado com sucesso.",  //Message
                new {  //Data
                        Id = customer.Id,
                        Name = name,
                        Email = email
                    });
        }
    }
}
