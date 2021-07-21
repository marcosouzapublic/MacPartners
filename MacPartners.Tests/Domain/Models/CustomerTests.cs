using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacPartners.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacPartners.Domain.Repositories;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Models.Enums;
using MacPartners.Tests.Infra.Repositories;
using MacPartners.Domain.Models.Entities;

namespace MacPartners.Domain.Models.Tests
{
    [TestClass()]
    public class CustomerTests
    {
        private readonly ICustomerRepository _repository;
        private readonly Phone _phone;
        private readonly Email _email;
        private readonly ZipCode _zipCode;
        private readonly Company _company;
        private readonly Person _person;
        private readonly Address _address;
        private readonly Partner _partner;

        public CustomerTests()
        {
            _repository = new MockCustomerRepository();
            _phone = new Phone("(15) 3331-2700");
            _email = new Email("bruce@waynecorp.com");
            _zipCode = new ZipCode("18035000");
            _company = new Company(new Cnpj("05061448000139"), "Manchester Automação Comercial Ltda", "Manchester Automação", _phone);
            _person = new Person("Bruce", "Wayne", new Cpf("41598913816"), null, _email, _phone);
            _address = new Address(_zipCode, "Avenida Professor Arthur Fonseca", "808", "Jd. Faculdade", "", "Sorocaba", EState.SP);
            _partner = new Partner(_person);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var curCount = _repository.ToList().Count;
            _repository.Create(new Customer(_company, _address, _partner));
            var newCount = _repository.ToList().Count;

            Assert.AreNotEqual(curCount, newCount);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var curCount = _repository.ToList().Count;
            _repository.Delete(_repository.ToList().FirstOrDefault());
            var newCount = _repository.ToList().Count;

            Assert.AreNotEqual(curCount, newCount);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var customer = _repository.ToList().FirstOrDefault();
            _repository.Update(customer);

            Assert.IsNotNull(_repository.Find(customer.Id));
        }
    }
}