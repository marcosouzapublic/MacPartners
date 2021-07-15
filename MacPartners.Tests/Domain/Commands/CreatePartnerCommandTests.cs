using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacPartners.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacPartners.Domain.Repositories;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Tests.Infra.Repositories;
using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models;

namespace MacPartners.Domain.Commands.Tests
{
    [TestClass()]
    public class CreatePartnerCommandTests
    {
        private readonly IPartnerRepository _repository;
        private readonly Person _person;
        private readonly Person _personError;
        private readonly CreatePartnerCommand _command;

        public CreatePartnerCommandTests()
        {
            _repository = new MockPartnerRepository();
            _person = new Person("Bruce", "Wayne", new Cpf("41598913816"), null, new Email("batman@justiceleague.com"), new Phone("(11) 3333-3333"));
            _personError = new Person("Bruce", "Wayne", new Cpf("41598913816"), null, new Email("batman@justiceleague.com"), new Phone("(11) 333333-3333"));
            _command = new CreatePartnerCommand(_repository);
        }

        [TestMethod()]
        public void CreatePartnerTest()
        {
            var result = _command.CreatePartner(_person);

            Assert.IsTrue(result.Status == Models.Enums.TransactionStatus.Success);
        }

        [TestMethod()]
        public void CreatePartnerErrorTest()
        {
            var result = _command.CreatePartner(_personError);

            Assert.IsTrue(result.Status == Models.Enums.TransactionStatus.Error);
        }
    }
}