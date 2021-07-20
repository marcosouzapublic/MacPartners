using Flunt.Notifications;
using MacPartners.Domain.Models.ValueObjects;

namespace MacPartners.Domain.Interfaces
{
    public interface IEmail
    {
        Notification Send(Email to, string subject, string body);
    }
}
