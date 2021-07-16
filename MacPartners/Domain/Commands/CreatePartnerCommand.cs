using MacPartners.Controllers;
using MacPartners.Domain.Models;
using MacPartners.Domain.Models.Enums;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Queries;
using MacPartners.Domain.Repositories;
using System;

namespace MacPartners.Domain.Commands
{
    public class CreatePartnerCommand
    {
        private readonly IPartnerRepository _repository;
        private readonly PartnersQueries _queries;

        public CreatePartnerCommand(IPartnerRepository repository, PartnersQueries queries)
        {
            _repository = repository;
            _queries = queries;
        }

        public TransactionResponse<Partner> CreatePartner(Person person, UsersController usersController)
        {
            try
            {
                //1 - Checking if the partner already registred
                if(_queries.IsPartnerAlreadRegistredByCpf(person.Cpf.Number) || _queries.IsPartnerAlreadRegistredByEmail(person.Email.EmailAdress))
                    return new Models.ValueObjects.TransactionResponse<Partner>(new Exception("Parceiro já cadastrado"));

                //2 - Create a new Partner
                var partner = new Partner(person);
                partner.Create(_repository);

                //3 - Create a new User
                var userResponse = usersController.CreateAfterPartner(person);

                //4 - Checking if the User Creation was successfully
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
