using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacPartners.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacPartners.Domain.Repositories;
using MacPartners.Tests.Infra.Repositories;

namespace MacPartners.Domain.Queries.Tests
{
    [TestClass()]
    public class PartnersQueriesTests
    {
        private readonly IPartnerRepository _repository;
        private readonly PartnersQueries _queries;

        public PartnersQueriesTests()
        {
            _repository = new MockPartnerRepository();
            _queries = new PartnersQueries(_repository);
        }

        [TestMethod()]
        public void IsPartnerAlreadRegistredByCpfPositiveTest()
        {
            Assert.IsTrue(_queries.IsPartnerAlreadRegistredByCpf("41598913816"));
        }

        [TestMethod()]
        public void IsPartnerAlreadRegistredByCpfNegativeTest()
        {
            Assert.IsFalse(_queries.IsPartnerAlreadRegistredByCpf("429.244.180-46"));
        }

        [TestMethod()]
        public void IsPartnerAlreadRegistredByEmailPositiveTest()
        {
            Assert.IsTrue(_queries.IsPartnerAlreadRegistredByEmail("batman@justiceleague.com"));
        }

        [TestMethod()]
        public void IsPartnerAlreadRegistredByEmailNegativeTest()
        {
            Assert.IsFalse(_queries.IsPartnerAlreadRegistredByEmail("robin@justiceleague.com"));
        }
    }
}