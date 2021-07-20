using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MacPartners.Domain.Models.ValueObjects
{
    public class ZipCode : Notifiable<Notification>
    {
        public ZipCode()
        {

        }

        public ZipCode(string cep)
        {
            if (String.IsNullOrEmpty(cep))
                AddNotification("Number", "O CEP não pode ser vazio");
            else
            {
                Number = cep;

                if (!Valid())
                    AddNotification("Number", "O CEP não pode ser vazio");
                else
                {
                    Id = Guid.NewGuid();
                }
            }
        }

        public Guid Id { get; private set; }
        public string Number { get; private set; }

        public bool Valid()
        {
            Regex Rgx = new Regex(@"^\d{5}-\d{3}$");

            if (!Rgx.IsMatch(Number))
                return false;

            else
                return true;
        }
    }
}
