using MacPartners.Controllers;
using MacPartners.Domain.Models;
using MacPartners.Domain.Models.Enums;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Repositories;
using System;

namespace MacPartners.Domain.Commands
{
    public class CreatePartnerCommand
    {
        private readonly IPartnerRepository _repository;

        public CreatePartnerCommand(IPartnerRepository repository)
        {
            _repository = repository;
        }

        public TransactionResponse<Partner> CreatePartner(Person person, UsersController usersController)
        {
            try
            {
                //1 - Create a new Partner
                var partner = new Partner(person);
                partner.Create(_repository);

                //2 - Create a new User
                var userResponse = usersController.CreateAfterPartner(person);

                //3 - Checking if the User Creation was successfully
                if(userResponse.Status == TransactionStatus.Success)
                    _repository.SaveChanges();
                else
                {
                    var partnerResponse = new Models.ValueObjects.TransactionResponse<Partner>(partner);
                    partnerResponse.IncludeNotifications(userResponse.Notifications);

                    return partnerResponse;
                }

                return new Models.ValueObjects.TransactionResponse<Partner>(partner);
            }
            catch (Exception exception)
            {
                return new Models.ValueObjects.TransactionResponse<Partner>(exception);
            }
        }
    }
}
