using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MacPartners.Domain.Models.ValueObjects
{
    public class Phone : Notifiable<Notification>
    {
        public Phone()
        {

        }

        public Phone(int areaCode, int number)
        {
            var phoneNumber = new StringBuilder().Append(areaCode).Append(number).ToString();

            if (!Regex.Match(phoneNumber, @"^(\+[0-9])$").Success)
                AddNotification("Number", "Número de telefone inválido");

            if (IsValid)
            {
                AreaCode = areaCode;
                Number = number;
                Id = Guid.NewGuid();
            }
        }

        public Phone(string phoneNumber)
        {
            if (String.IsNullOrEmpty(phoneNumber) || !Regex.Match(phoneNumber, @"^\d{5}-\d{3}$").Success)
                AddNotification("Number", "Número de telefone inválido");

            if (IsValid)
            {
                AreaCode = ExtractAreaCode(phoneNumber);
                Number = ExtractNumber(phoneNumber);
                Id = Guid.NewGuid();
            }
        }

        [Key]
        public Guid Id { get; private set; }
        public int AreaCode { get; private set; }
        public int Number { get; private set; }

        public override string ToString()
        {
            if (Number.ToString().Length == 8)
            {
                return new StringBuilder()
                .Append('(')
                .Append(AreaCode)
                .Append(')')
                .Append(' ')
                .Append(Number.ToString().Substring(0,4))
                .Append('-')
                .Append(Number.ToString().Substring(4,4))
                .ToString();
            }
            else
            {
                return new StringBuilder()
                .Append('(')
                .Append(AreaCode)
                .Append(')')
                .Append(' ')
                .Append(Number.ToString().Substring(0, 5))
                .Append('-')
                .Append(Number.ToString().Substring(5, 4))
                .ToString();
            }
        }

        public int ExtractAreaCode(string phoneNumber)
        {
            phoneNumber = ClearPhoneNumber(phoneNumber);

            return int.Parse(phoneNumber.Substring(0,2));
        }

        public int ExtractNumber(string phoneNumber)
        {
            phoneNumber = ClearPhoneNumber(phoneNumber);

            return int.Parse(phoneNumber.Substring(2, phoneNumber.Length - 2));
        }

        public string ClearPhoneNumber(string phoneNumber)
        {
            phoneNumber = phoneNumber
                .Replace("(", "")
                .Replace(")", "")
                .Replace(" ", "")
                .Replace("-", "");

            return phoneNumber;
        }

        public void ChangePhone(string phoneNumber)
        {
            if (String.IsNullOrEmpty(phoneNumber) || !Regex.Match(phoneNumber, @"^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$").Success)
                AddNotification("Number", "Número de telefone inválido");

            if (IsValid)
            {
                AreaCode = ExtractAreaCode(phoneNumber);
                Number = ExtractNumber(phoneNumber);
            }                
        }
    }
}
