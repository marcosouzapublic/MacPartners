using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Repositories;
using MacPartners.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MacPartners.Infra.Repositories
{
    public class PartnerRepository : IPartnerRepository
    {
        private readonly MacPartnersContext _context;

        public PartnerRepository(MacPartnersContext context)
        {
            _context = context;
        }

        public void Create(Partner partner)
        {
            _context.Partners.Add(partner);
        }

        public void Delete(Partner partner)
        {
            _context.Partners.Remove(partner);
        }

        public Partner Find(Guid id)
        {
            return _context.Partners.Find(id);
        }

        public IList<Partner> ToList()
        {
            return _context.Partners.ToList();
        }

        public IList<Partner> ToList(Expression<Func<Partner, bool>> expression)
        {
            return _context.Partners.Where(expression).ToList();
        }

        public void Update(Partner partner)
        {
            _context.Entry(partner).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
