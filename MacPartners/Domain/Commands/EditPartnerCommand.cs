using MacPartners.Controllers;
using MacPartners.Domain.Models;
using MacPartners.Domain.Models.Enums;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Repositories;
using System;

namespace MacPartners.Domain.Commands
{
    public class EditPartnerCommand
    {
        private readonly IPartnerRepository _repository;

        public EditPartnerCommand(IPartnerRepository repository)
        {
            _repository = repository;
        }

        public TransactionResponse<Partner> EditPartner(Partner partner)
        {
            try
            {
                partner.Update(_repository);
                _repository.SaveChanges();

                return new Models.ValueObjects.TransactionResponse<Partner>(partner);
            }
            catch (Exception exception)
            {
                return new Models.ValueObjects.TransactionResponse<Partner>(exception);
            }
        }
    }
}
