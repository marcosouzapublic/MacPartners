using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models;
using MacPartners.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacPartners.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void SaveChanges();
    }
}
