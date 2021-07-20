using MacPartners.Domain.Models;
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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MacPartnersContext _context;

        public CustomerRepository(MacPartnersContext context)
        {
            _context = context;
        }

        public void Create(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public Customer Find(Guid id)
        {
            return _context.Customers.Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IList<Customer> ToList()
        {
            return _context.Customers.ToList();
        }

        public IList<Customer> ToList(Expression<Func<Customer, bool>> expression)
        {
            return _context.Customers.Where(expression).ToList();
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
        }
    }
}
