using MacPartners.Domain.Models;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Repositories;
using MacPartners.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacPartners.Domain.Commands
{
    public class BlockPartnerCommand
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IUserRepository _userRepository;

        public BlockPartnerCommand(IPartnerRepository repository, IUserRepository userRepository)
        {
            _partnerRepository = repository;
            _userRepository = userRepository;
        }

        public TransactionResponse<Partner> BlockPartner(Partner partner)
        {
            try
            {
                //1 - Blocking partner
                partner.Block(_partnerRepository);
                _partnerRepository.SaveChanges();

                //2 - Blocking user
                var user = _userRepository.ToList(u => u.Person.Id == partner.Person.Id).FirstOrDefault();
                user.Block(_userRepository);
                _userRepository.SaveChanges();

                return new Models.ValueObjects.TransactionResponse<Partner>(partner);
            }
            catch (Exception exception)
            {
                return new Models.ValueObjects.TransactionResponse<Partner>(exception);
            }
        }

        public TransactionResponse<Partner> UnblockPartner(Partner partner)
        {
            try
            {
                //1 - Blocking partner
                partner.Unblock(_partnerRepository);
                _partnerRepository.SaveChanges();

                //2 - Blocking user
                var user = _userRepository.ToList(u => u.Person == partner.Person).FirstOrDefault();
                user.Unblock(_userRepository);
                _userRepository.SaveChanges();

                return new Models.ValueObjects.TransactionResponse<Partner>(partner);
            }
            catch (Exception exception)
            {
                return new Models.ValueObjects.TransactionResponse<Partner>(exception);
            }
        }
    }
}
