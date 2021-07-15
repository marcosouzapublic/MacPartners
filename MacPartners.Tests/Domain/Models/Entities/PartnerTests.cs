using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Tests.Infra.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacPartners.Tests.Domain.Models.Entities
{
    [TestClass]
    public class PartnerTests
    {
        private readonly IRepository<Partner> _repository;

        public PartnerTests()
        {
            _repository = new MockPartnerRepository();
        }

        [TestMethod]
        public void IfPartnerIsCreated()
        {
            var currentPartnersCount = _repository.ToList().Count();
            var partner = new Partner(new Person("Diana", "Princess", new Cpf("11111111180"), null, new Email("wonderwoman@justiceleague.com"), new Phone("(15) 99111-1111")));
            partner.Create(_repository);
            var newPartnersCount = _repository.ToList().Count();

            Assert.AreNotEqual(currentPartnersCount, newPartnersCount);
        }

        [TestMethod]
        public void IfPartnerIsDeleted()
        {
            var currentPartnersCount = _repository.ToList().Count();
            var partner = _repository.ToList().FirstOrDefault();
            partner.Delete(_repository);
            var newPartnersCount = _repository.ToList().Count();

            Assert.AreNotEqual(currentPartnersCount, newPartnersCount);
        }

        [TestMethod]
        public void IfPartnerIsBlocked()
        {
            var partner = _repository.ToList().FirstOrDefault();
            partner.Block(_repository);

            Assert.IsTrue(partner.IsBlocked);
        }

        [TestMethod]
        public void IfPartnerIsUnblocked()
        {
            var partner = _repository.ToList().FirstOrDefault();
            partner.Block(_repository);
            partner.Unblock(_repository);

            Assert.IsFalse(partner.IsBlocked);
        }
    }
}
