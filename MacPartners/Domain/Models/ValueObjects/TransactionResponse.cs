using Flunt.Notifications;
using MacPartners.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPartners.Domain.Models.ValueObjects
{
    public class TransactionResponse<T> where T : Notifiable<Notification>
    {
        public TransactionResponse(T transactionalObject)
        {
            if (transactionalObject.IsValid)
                Status = TransactionStatus.Success;
            else
                Status = TransactionStatus.Error;

            FillMessage(transactionalObject.Notifications);
            Notifications = transactionalObject.Notifications.ToList();
        }

        public TransactionResponse(Exception exception)
        {
            Status = TransactionStatus.Error;
            Message = exception.Message;
        }

        public TransactionStatus Status { get; private set; }
        public string Message { get; private set; }

        public  List<Notification> Notifications { get; private set; }

        private void FillMessage(IReadOnlyCollection<Notification> notifications)
        {
            var message = new StringBuilder();
            foreach (var notification in notifications)
            {
                message.AppendLine(notification.Message);
            }

            Message = message.ToString();
        }

        public void IncludeNotifications(ICollection<Notification> notifications)
        {
            Notifications.AddRange(notifications.ToList());
        }
    }
}
