using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacPartners.Domain.Models.ValueObjects
{
    public class Company : Notifiable<Notification>
    {
        public Company()
        {

        }

        public Company(Cnpj cnpj, string name, string tradeName, Phone phone)
        {
            AddNotifications(cnpj.Notifications);

            if(IsValid)
            {
                Cnpj = cnpj;
                Name = name;
                TradeName = tradeName;
                Phone = phone;
                Id = Guid.NewGuid();
            }
        }

        public Guid Id { get; private set; }
        public virtual Cnpj Cnpj { get; private set; }
        public string Ie { get; private set; }
        public string Name { get; private set; }
        public string TradeName { get; private set; }
        public virtual Phone Phone { get; private set; }
    }
}
