using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacPartners.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MacPartners.Domain.Interfaces;
using MacPartners.Tests.Infra.Repositories;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Infra.Services;
using System.Linq;

namespace MacPartners.Domain.Models.Entities.Tests
{
    [TestClass()]
    public class UserTests
    {
        private readonly IRepository<User> _repository;
        private readonly ICrypter _crypter;

        public UserTests()
        {
            _repository = new MockUserRepository();
            _crypter = new CrypterService();
        }

        [TestMethod()]
        public void IfUserIsCreated()
        {
            var newUser = new User("5554466", new Person("Berry", "Allen", new Cpf("11111111180"), null, new Email("flash@justiceleague.com")), _crypter);
            newUser.Create(_repository);

            Assert.IsNotNull(_repository.Find(newUser.Id));
        }

        [TestMethod()]
        public void IfUserIsDeleted()
        {
            var user = _repository.ToList().FirstOrDefault();
            user.Delete(_repository);

            Assert.IsNull(_repository.Find(user.Id));
        }

        [TestMethod()]
        public void IfUserIsBlocked()
        {
            var user = _repository.ToList().FirstOrDefault();
            user.Block(_repository);

            Assert.IsTrue(user.IsBlocked);
        }

        [TestMethod()]
        public void IfUserIsUnblocked()
        {
            var user = _repository.ToList().FirstOrDefault();
            user.Block(_repository);
            user.Unblock(_repository);

            Assert.IsFalse(user.IsBlocked);
        }

        [TestMethod]
        public void IfUserPasswordIsEncrypted()
        {
            var newUser = new User("5554466", new Person("Berry", "Allen", new Cpf("11111111180"), null, new Email("flash@justiceleague.com")), _crypter);

            Assert.AreNotEqual("5554466", newUser.Password);
        }
    }
}