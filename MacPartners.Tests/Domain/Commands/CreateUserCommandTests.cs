﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacPartners.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacPartners.Domain.Repositories;
using MacPartners.Domain.Models.Entities;
using MacPartners.Tests.Infra.Repositories;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Interfaces;
using MacPartners.Infra.Services;

namespace MacPartners.Domain.Commands.Tests
{
    [TestClass()]
    public class CreateUserCommandTests
    {
        private readonly IUserRepository _repository;
        private readonly User _user;
        private readonly User _errorUser;
        private readonly CreateUserCommand _command;
        private readonly Person _person;
        private readonly ICrypter _crypter;

        public CreateUserCommandTests()
        {
            _person = new Person("Bruce", "Wayne", new Cpf("41598913816"), null, new Email("batman@justiceleague.com"), new Phone("(11) 3333-3333"));
            _crypter = new CrypterService();
            _repository = new MockUserRepository();
            _command = new CreateUserCommand(_repository, _crypter);
        }

        [TestMethod()]
        public void CreateUserTest()
        {
            var result = _command.CreateUser("bAtman123", _person);

            Assert.IsTrue(result.Status == Models.Enums.TransactionStatus.Success);
        }

        [TestMethod()]
        public void CreateUserErrorTest()
        {
            var result = _command.CreateUser(null, _person);

            Assert.IsTrue(result.Status == Models.Enums.TransactionStatus.Error);
        }
    }
}