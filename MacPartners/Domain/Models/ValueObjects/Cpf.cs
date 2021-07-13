using Flunt.Notifications;
using Flunt.Validations;
using MacPartners.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MacPartners.Domain.Models.ValueObjects
{
    public class Cpf : Notifiable<Notification>, IDocument
    {
        public Cpf(string number)
        {
            AddNotifications(new Contract<Notification>()
               .IsNullOrEmpty(number, "Cpf", "O CPF da pessoa não pode ser vazio")
           );

            Number = number;
            Id = Guid.NewGuid();

            if (!IsValid())
                AddNotification("Cpf", "O CPF é inválido");
        }

        [Key]
        public Guid Id { get; private set; }
        public string Number { get; private set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
