using MacPartners.Domain.Models;
using MacPartners.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacPartners.Domain.Queries
{
    public class PartnersQueries
    {
        private readonly IPartnerRepository _repository;

        public PartnersQueries(IPartnerRepository repository)
        {
            _repository = repository;
        }

        public IList<Partner> AllPartners()
        {
            var partners = _repository.ToList();

            return partners;
        }

        public IList<Partner> BlockedPartners()
        {
            var partners = _repository.ToList(p => p.IsBlocked);

            return partners;
        }

        public IList<Partner> UnblockedPartners()
        {
            var partners = _repository.ToList(p => !p.IsBlocked);

            return partners;
        }

        public bool IsPartnerAlreadRegistredByCpf(string cpf)
        {
            var partners = _repository.ToList(p => p.Person.Cpf.Number.Equals(cpf.Replace(".", "").Replace("-","")));

            return partners.Count > 0;
        }

        public bool IsPartnerAlreadRegistredByEmail(string email)
        {
            var partners = _repository.ToList(p => p.Person.Email.EmailAdress.Equals(email));

            return partners.Count > 0;
        }

        public Partner PartnerByUserEmail(string userEmail)
        {
            var partners = _repository.ToList(p => p.Person.Email.EmailAdress.Equals(userEmail));

            return partners.FirstOrDefault();
        }
    }
}
