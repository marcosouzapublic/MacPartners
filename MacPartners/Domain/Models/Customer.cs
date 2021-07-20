using Flunt.Notifications;
using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models.ValueObjects;
using System;

namespace MacPartners.Domain.Models
{
    public class Customer : Notifiable<Notification>, IEntity<Customer>
    {
        public Customer()
        {

        }

        public Customer(Person person, Address address, Partner partner)
        {
            AddNotifications(person.Notifications);
            AddNotifications(address.Notifications);
            AddNotifications(partner.Notifications);

            if (IsValid)
            {
                Person = person;
                Address = address;
                Partner = partner;
                CreatedAt = DateTime.Now;
                Id = Guid.NewGuid();
            }
        }

        public Customer(Company company, Address address, Partner partner)
        {
            AddNotifications(company.Notifications);
            AddNotifications(address.Notifications);
            AddNotifications(partner.Notifications);

            if (IsValid)
            {
                Company = company;
                Address = address;
                Partner = partner;
                CreatedAt = DateTime.Now;
                Id = Guid.NewGuid();
            }
        }

        public Guid Id {get; private set;}
        public virtual Person Person { get; private set; }
        public virtual Company Company { get; private set; }
        public virtual Address Address { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public virtual Partner Partner { get; private set; }

        public void Create(IRepository<Customer> repository)
        {
            throw new NotImplementedException();
        }

        public void Delete(IRepository<Customer> repository)
        {
            throw new NotImplementedException();
        }

        public void Update(IRepository<Customer> repository)
        {
            throw new NotImplementedException();
        }
    }
}
