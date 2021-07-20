using Flunt.Notifications;
using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models.Enums;
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

        public User(string password, Person person, ICrypter crypter, EUserRole userRole)
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
                UserRole = userRole;
                Id = Guid.NewGuid();
            }
        }

        [Key]
        public Guid Id { get; private set; }
        public virtual Person Person { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? BlockedAt { get; private set; }
        public string Password { get; private set; }
        public bool IsBlocked { get; private set; }
        public EUserRole UserRole { get; private set; }


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

        public void Block(IRepository<User> repository)
        {
            IsBlocked = true;
            BlockedAt = DateTime.Now;
            Update(repository);
        }

        public void Unblock(IRepository<User> repository)
        {
            IsBlocked = false;
            BlockedAt = null;
            Update(repository);
        }

        public void ChangePassword(string password, ICrypter crypter, IRepository<User> repository)
        {
            Password = crypter.Encrypt(password);
            Update(repository);
        }
    }
}
