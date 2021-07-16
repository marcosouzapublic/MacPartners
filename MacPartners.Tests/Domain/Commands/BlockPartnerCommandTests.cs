using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacPartners.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacPartners.Domain.Repositories;
using MacPartners.Domain.Models;
using MacPartners.Tests.Infra.Repositories;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Models.Enums;

namespace MacPartners.Domain.Commands.Tests
{
    [TestClass()]
    public class BlockPartnerCommandTests
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IUserRepository _userRepository;
        private readonly BlockPartnerCommand _command;

        public BlockPartnerCommandTests()
        {
            _partnerRepository = new MockPartnerRepository();
            _userRepository = new MockUserRepository();
            _command = new BlockPartnerCommand(_partnerRepository, _userRepository);
        }

        [TestMethod()]
        public void BlockPartnerTest()
        {
            var partner = _partnerRepository.ToList().FirstOrDefault();
            var result = _command.BlockPartner(partner);

            Assert.IsTrue(result.Status == TransactionStatus.Success);
        }

        [TestMethod()]
        public void UnblockPartnerTest()
        {
            var partner = _partnerRepository.ToList().FirstOrDefault();
            var result = _command.UnblockPartner(partner);

            Assert.IsTrue(result.Status == TransactionStatus.Success);
        }
    }
}