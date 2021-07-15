using Flunt.Notifications;
using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    }
}
