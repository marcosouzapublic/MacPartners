using Flunt.Notifications;
using Flunt.Validations;
using MacPartners.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacPartners.Domain.Models.ValueObjects
{
    public class Address : Notifiable<Notification>
    {
        public Address()
        {

        }

        public Address(ZipCode zipCode, string street, string number, string district, string complement, string city, EState state)
        {
            AddNotifications(zipCode.Notifications);
            AddNotifications(new Contract<Notification>()
                .IsNullOrEmpty(street, "Street", "O logradouro não pode ser vazio")
                .IsNullOrEmpty(number, "Number", "O número não pode ser vazio")
                .IsNullOrEmpty(district, "District", "O bairro não pode ser vazio")
                .IsNullOrEmpty(city, "City", "A cidade não pode ser vazia")
            );

            if (IsValid)
            {
                ZipCode = zipCode;
                Street = street;
                Number = number;
                District = district;
                Complement = complement;
                City = city;
                State = state;
                Id = Guid.NewGuid();
            }
        }

        public Guid Id { get; private set; }
        public virtual ZipCode ZipCode { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string District { get; private set; }
        public string Complement { get; private set; }
        public string City { get; private set; }
        public EState State { get; private set; }
    }
}
