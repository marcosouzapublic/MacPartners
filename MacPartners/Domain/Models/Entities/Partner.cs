using Flunt.Notifications;
using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MacPartners.Domain.Models
{
    public class Partner : Notifiable<Notification>, IEntity<Partner>
    {
        public Partner()
        {

        }

        public Partner(Person person)
        {
            AddNotifications(person.Notifications);

            if (IsValid)
            {
                Person = person;
                CreatedAt = DateTime.Now;
                Id = Guid.NewGuid();
            }
        }

        [Key]
        public Guid Id { get; private set; }
        public virtual Person Person { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? BlockedAt { get; private set; }
        public bool IsBlocked { get; private set; }

        public void Create(IRepository<Partner> repository)
        {
            if(IsValid)
                repository.Create(this);
        }

        public void Delete(IRepository<Partner> repository)
        {
            if (IsValid)
                repository.Delete(this);
        }

        public void Update(IRepository<Partner> repository)
        {
            if (IsValid)
                repository.Update(this);
        }

        public void Block(IRepository<Partner> repository)
        {
            IsBlocked = true;
            BlockedAt = DateTime.Now;
            Update(repository);
        }

        public void Unblock(IRepository<Partner> repository)
        {
            IsBlocked = false;
            BlockedAt = null;
            Update(repository);
        }

        public void EditPerson(string name, string lastName, string phone, DateTime? birthday)
        {
            if (String.IsNullOrEmpty(name))
                AddNotification("Name", "O nome não pode ser vazio");

            if (String.IsNullOrEmpty(lastName))
                AddNotification("Name", "O sobrenome não pode ser vazio");
                
            if (String.IsNullOrEmpty(phone) || !Regex.Match(phone, @"^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$").Success)
                AddNotification("Number", "Número de telefone inválido");

            if (IsValid)
            {
                Person.ChangeName(name);
                Person.ChangeLastName(lastName);
                Person.Phone.ChangePhone(phone);
                Person.ChangeBirthday(birthday);
            }
        }
    }
}
