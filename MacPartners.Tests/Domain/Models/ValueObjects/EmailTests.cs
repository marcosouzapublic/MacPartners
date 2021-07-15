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
    public class EmailTests
    {
        private readonly Email _email;

        public EmailTests()
        {
            _email = new Email("batman@justiceleague.com");
        }

        [TestMethod()]
        public void ShortEmailTest()
        {
            Assert.AreEqual(_email.ShortEmail(), "batma***.com");
        }
    }
}