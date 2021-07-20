using MacPartners.Domain.Models;
using MacPartners.Domain.Models.Enums;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MacPartners.Tests.Infra.Repositories
{
    class MockCustomerRepository : ICustomerRepository
    {
        private IList<Customer> _customers;

        public MockCustomerRepository()
        {
            _customers = new List<Customer>();

            var phone = new Phone("(15) 3331-2700");
            var email = new Email("bruce@waynecorp.com");
            var zipCode = new ZipCode("18035000");
            var company = new Company(new Cnpj("05061448000139"), "Manchester Automação Comercial Ltda", "Manchester Automação", phone);
            var person = new Person("Bruce", "Wayne", new Cpf("41598913816"), null, email, phone);
            var address = new Address(zipCode, "Avenida Professor Arthur Fonseca", "808", "Jd. Faculdade", "", "Sorocaba", EState.SP);
            var partner = new Partner(person);

            _customers.Add(new Customer(company, address, partner));
            _customers.Add(new Customer(person, address, partner));
        }

        public void Create(Customer customer)
        {
            _customers.Add(customer);
        }

        public void Delete(Customer customer)
        {
            _customers.Remove(customer);
        }

        public Customer Find(Guid id)
        {
            return _customers.Where(c => c.Id == id).FirstOrDefault();
        }

        public void SaveChanges()
        {
            
        }

        public IList<Customer> ToList()
        {
            return _customers;
        }

        public IList<Customer> ToList(Expression<Func<Customer, bool>> expression)
        {
            Func<Customer, bool> func = expression.Compile();

            return _customers.Where(func).ToList();
        }

        public void Update(Customer customer)
        {
            Delete(customer);
            Create(customer);
        }
    }
}
