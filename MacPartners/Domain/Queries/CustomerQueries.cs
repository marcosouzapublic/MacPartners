using MacPartners.Domain.Models;
using MacPartners.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacPartners.Domain.Queries
{
    public class CustomerQueries
    {
        private readonly ICustomerRepository _repository;

        public CustomerQueries(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public IList<Customer> AllCustomers()
        {
            return _repository.ToList();
        }

        public IList<Customer> PartnerCustomers(Partner partner)
        {
            return _repository.ToList(p => p.Partner.Id == partner.Id);
        }
    }
}
