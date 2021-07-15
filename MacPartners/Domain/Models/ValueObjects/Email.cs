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
    public class Email : Notifiable<Notification>
    {
        public Email() { }
        public Email(string emailAdress) 
        {
            AddNotifications(new Contract<Notification>()
                .IsNotNullOrEmpty(emailAdress, "EmailAdress", "O endereço de e-mail não pode ser vazio")
                .IsEmail(emailAdress, "EmailAdress", "O e-mail não é válido")
            );

            if (IsValid)
            {
                EmailAdress = emailAdress;
                Id = Guid.NewGuid();
            }
        }

        [Key]
        public Guid Id { get; private set; }
        public string EmailAdress { get; private set; }

        public string ShortEmail()
        {
            const int charFromBegin = 5;
            const int charFromEnd = 4;

            return EmailAdress.Substring(0, charFromBegin) + "***" + EmailAdress.Substring(EmailAdress.Length - charFromEnd, charFromEnd);
        }
    }
}
