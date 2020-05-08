using BaltaStore.Shared.Commands;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs
{
    public class CreateCustomerCommand : Notifiable, ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public bool Valid()
        {
            AddNotifications(new ValidationContract()
                .HasMinLen(FirstName, 3, "FirstName", "O nome deve te pelo menos 3 caracteres")
                .HasMinLen(LastName, 3, "LastName", "O último nome deve te pelo menos 3 caracteres")

                .IsEmail(Email, "Address", "O e-mail inválido")

                .HasLen(Document,11, "Documente", "CPF inválido")
            );
            return IsValid;
        }
    }
}
