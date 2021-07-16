using Flunt.Notifications;
using Flunt.Validations;
using MacPartners.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPartners.Domain.Models.ValueObjects
{
    public class Cpf : Notifiable<Notification>, IDocument
    {
        public Cpf(string number)
        {
            AddNotifications(new Contract<Notification>()
               .IsNotNullOrEmpty(number, "Cpf", "O CPF da pessoa não pode ser vazio")
            );

            if(Notifications.Count == 0)
            {
                Number = number;
                Id = Guid.NewGuid();

                if (!IsValid())
                    AddNotification("Cpf", "O CPF é inválido");
            }
        }

        [Key]
        public Guid Id { get; private set; }
        public string Number { get; private set; }

        public bool IsValid()
        {

            if (String.IsNullOrEmpty(Number))
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempNumber;
            string digito;
            int soma;
            int resto;

            Number = Number.Trim();
            Number = Number.Replace(".", "").Replace("-", "");

            if (Number.Length != 11)
                return false;

            tempNumber = Number.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempNumber[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempNumber = tempNumber + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempNumber[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return Number.EndsWith(digito);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(Number.ToString().Substring(0, 3))
                .Append('.')
                .Append(Number.ToString().Substring(3, 3))
                .Append('.')
                .Append(Number.ToString().Substring(6, 3))
                .Append('-')
                .Append(Number.ToString().Substring(9, 2))
                .ToString();
        }
    }
}
