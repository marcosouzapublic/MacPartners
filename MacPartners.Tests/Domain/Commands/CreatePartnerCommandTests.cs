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
using MacPartners.Domain.Queries;
using MacPartners.Infra.Services;

namespace MacPartners.Domain.Commands.Tests
{
    [TestClass()]
    public class CreatePartnerCommandTests
    {
        private readonly IPartnerRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly Person _person;
        private readonly Person _personError;
        private readonly Person _personErrorCpfDuplicated;
        private readonly Person _personErrorEmailDuplicated;
        private readonly CreatePartnerCommand _command;
        private readonly PartnersQueries _queries;
        private readonly ICrypter _crypter;

        public CreatePartnerCommandTests()
        {
            _repository = new MockPartnerRepository();
            _userRepository = new MockUserRepository();
            _queries = new PartnersQueries(_repository);
            _person = new Person("Tony", "Stark", new Cpf("34965575083"), null, new Email("ironman@avangers.com"), new Phone("(11) 3333-3333"));
            _personError = new Person("Bruce", "", new Cpf("71679984004"), null, new Email("batman@dc.com"), new Phone("(11) 3333-3333"));
            _personErrorCpfDuplicated = new Person("Bruce", "Wayne", new Cpf("41598913816"), null, new Email("batman@dc.com"), new Phone("(11) 333333-3333"));
            _personErrorEmailDuplicated = new Person("Bruce", "Wayne", new Cpf("41598913816"), null, new Email("batman@dc.com"), new Phone("(11) 333333-3333"));
            _command = new CreatePartnerCommand(_repository, _queries);
            _crypter = new CrypterService();
        }

        [TestMethod()]
        public void CreatePartnerTest()
        {
            var result = _command.CreatePartner(_person, new Controllers.UsersController(_userRepository, _crypter));

            Assert.IsTrue(result.Status == Models.Enums.TransactionStatus.Success);
        }

        [TestMethod()]
        public void CreateDuplicatedCpfPartnerTest()
        {
            var result = _command.CreatePartner(_personErrorCpfDuplicated, new Controllers.UsersController(_userRepository, _crypter));

            Assert.IsTrue(result.Status == Models.Enums.TransactionStatus.Error);
        }

        [TestMethod()]
        public void CreateDuplicatedEmailPartnerTest()
        {
            var result = _command.CreatePartner(_personErrorEmailDuplicated, new Controllers.UsersController(_userRepository, _crypter));

            Assert.IsTrue(result.Status == Models.Enums.TransactionStatus.Error);
        }

        [TestMethod()]
        public void CreatePartnerErrorTest()
        {
            var result = _command.CreatePartner(_personError, new Controllers.UsersController(_userRepository, _crypter));

            Assert.IsTrue(result.Status == Models.Enums.TransactionStatus.Error);
        }
    }
}