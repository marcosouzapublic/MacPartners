using Flunt.Notifications;
using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MacPartners.Domain.Models.Entities
{
    public class User : Notifiable<Notification>, IEntity<User>
    {
        public User()
        {

        }

        public User(string password, Person person, ICrypter crypter)
        {
            if (String.IsNullOrEmpty(password)) 
            {
                AddNotification("Password", "A senha não pode ser vazia");
            }
            else
            {
                CreatedAt = DateTime.Now;
                Password = crypter.Encrypt(password);
                Person = person;
                Id = Guid.NewGuid();
            }
        }

        [Key]
        public Guid Id { get; private set; }
        public Person Person { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? BlockedAt { get; private set; }
        public string Password { get; private set; }


        public void Create(IRepository<User> repository)
        {
            if (IsValid)
                repository.Create(this);
        }

        public void Delete(IRepository<User> repository)
        {
            repository.Delete(this);
        }

        public void Update(IRepository<User> repository)
        {
            if (IsValid)
                repository.Update(this);
        }
    }
}
