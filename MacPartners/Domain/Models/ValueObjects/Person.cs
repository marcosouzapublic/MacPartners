using Flunt.Notifications;
using Flunt.Validations;
using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MacPartners.Domain.Models.ValueObjects
{
    public class Person : Notifiable<Notification>
    {
        public Person() { }

        public Person(string name, string lastName, Cpf cpf, DateTime? birthday, Email email, Phone phone)
        {
            AddNotifications(new Contract<Notification>()
                .IsNotNullOrEmpty(name, "Name", "O nome da pessoa não pode ser vazio")
                .IsNotNullOrEmpty(lastName, "LastName", "O sobrenome da pessoa não pode ser vazio")
                .IsNotNull(cpf, "Cpf", "O CPF da pessoa não pode ser vazio")
            );

            if (!cpf.IsValid())
                AddNotification("Cpf", "O CPF é inválido");

            if(!email.IsValid)
                AddNotification("Email", "O e-mail é inválido");

            if(!phone.IsValid)
                AddNotification("Phone", "O telefone é inválido");

            Name = name;
            LastName = lastName;
            Cpf = cpf;
            Birthday = birthday;
            Email = email;
            Phone = phone;
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public virtual Cpf Cpf { get; private set; }
        public DateTime? Birthday { get; private set; }
        public virtual Email Email { get; private set; }
        public virtual Phone Phone { get; private set; }
    }
}
