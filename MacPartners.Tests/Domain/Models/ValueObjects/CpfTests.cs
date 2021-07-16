using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacPartners.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacPartners.Domain.Models.ValueObjects.Tests
{
    [TestClass()]
    public class CpfTests
    {
        private readonly Cpf _cpf;
        private readonly Cpf _invalidCpf;
        private readonly Cpf _cpfWithAccents;

        public CpfTests()
        {
            _cpf = new Cpf("41598913816");
            _invalidCpf = new Cpf("41598913815");
            _cpfWithAccents = new Cpf("415.989.138-16");
        }

        [TestMethod()]
        public void IsValidTest()
        {
            Assert.IsTrue(_cpf.IsValid());
        }

        [TestMethod()]
        public void IsValidWithAccentsTest()
        {
            Assert.IsTrue(_cpfWithAccents.IsValid());
        }

        [TestMethod()]
        public void IsInvalidTest()
        {
            Assert.IsFalse(_invalidCpf.IsValid());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(_cpf.ToString(), "415.989.138-16");
        }
    }
}