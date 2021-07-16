using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacPartners.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacPartners.Domain.Repositories;
using MacPartners.Tests.Infra.Repositories;
using MacPartners.Domain.Models;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Models.Enums;

namespace MacPartners.Domain.Commands.Tests
{
    [TestClass()]
    public class EditPartnerCommandTests
    {
        private readonly IPartnerRepository _repository;
        private readonly EditPartnerCommand _command;
        private readonly Partner _partner;

        public EditPartnerCommandTests()
        {
            _repository = new MockPartnerRepository();
            _command = new EditPartnerCommand(_repository);
            _partner = new Partner(new Person("Clark", "Kenty", new Cpf("11111111111"), DateTime.Parse("25-11-1967"), new Email("superman@justiceleague.com"), new Phone("(15) 99111-1111")));
        }

        [TestMethod()]
        public void EditPartnerTest()
        {
            var response = _command.EditPartner(_partner);

            Assert.IsTrue(response.Status == TransactionStatus.Success);
        }
    }
}