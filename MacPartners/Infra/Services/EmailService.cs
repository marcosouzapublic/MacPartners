using Flunt.Notifications;
using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MacPartners.Infra.Services
{
    public class EmailService : IEmail
    {
        private readonly MailMessage _mailMessage;
        private readonly SmtpClient _smtpClient;
        private readonly Email _from;

        public EmailService()
        {
            _mailMessage = new MailMessage();
            _smtpClient =  new SmtpClient();
            _from = new Email("financareapp@gmail.com");
        }

        public Notification Send(Email to, string subject, string body)
        {
            ConfigureMailMessage(to, subject, body);
            ConfigureSmtpClient();

            try
            {
                _smtpClient.Send(_mailMessage);
                return new Notification("SendEmailError", "E-mail enviado com sucesso");
            }
            catch (Exception exception)
            {
                return new Notification("SendEmailError", "Falha ao enviar E-mail: " + exception.Message);                
            }
            
        }

        private void ConfigureMailMessage(Email to, string subject, string body)
        {
            _mailMessage.Subject = subject;
            _mailMessage.From = new MailAddress(_from.EmailAdress);
            _mailMessage.To.Add(to.EmailAdress);
            _mailMessage.IsBodyHtml = true;
            _mailMessage.Body = body;
        }

        private void ConfigureSmtpClient()
        {
            _smtpClient.Port = 587;
            _smtpClient.Host = "smtp.gmail.com";
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.Credentials = new System.Net.NetworkCredential("financareapp@gmail.com", "Eli@ne87");
            _smtpClient.EnableSsl = true;
        }
    }
}
