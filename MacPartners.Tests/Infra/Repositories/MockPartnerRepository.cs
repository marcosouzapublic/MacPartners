using MacPartners.Domain.Models;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MacPartners.Tests.Infra.Repositories
{
    class MockPartnerRepository : IPartnerRepository
    {
        private IList<Partner> _partners;

        public MockPartnerRepository()
        {
            _partners = new List<Partner>();

            _partners.Add(new Partner(new Person("Bruce", "Wayne", new Cpf("41598913816"), DateTime.Parse("03-02-1994"), new Email("batman@justiceleague.com"), new Phone("(15) 99111-1111"))));
            _partners.Add(new Partner(new Person("Berry", "Allen", new Cpf("05839583898"), DateTime.Parse("18-04-1964"), new Email("flash@justiceleague.com"), new Phone("(15) 99111-1111"))));
            _partners.Add(new Partner(new Person("Clark", "Kent", new Cpf("11111111111"), DateTime.Parse("25-11-1967"), new Email("superman@justiceleague.com"), new Phone("(15) 99111-1111"))));
        }

        public void Create(Partner partner)
        {
            _partners.Add(partner);
        }

        public void Delete(Partner partner)
        {
            _partners.Remove(partner);
        }

        public Partner Find(Guid id)
        {
            return _partners.Where(p => p.Id == id).FirstOrDefault();
        }

        public void SaveChanges()
        {
            
        }

        public IList<Partner> ToList()
        {
            return _partners;
        }

        public IList<Partner> ToList(Expression<Func<Partner, bool>> expression)
        {
            Func<Partner, bool> func = expression.Compile();

            return _partners.Where(func).ToList();
        }

        public void Update(Partner partner)
        {
            var partnerToDelete = Find(partner.Id);
            Delete(partnerToDelete);
            Create(partner);
        }
    }
}
