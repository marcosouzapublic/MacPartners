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
    public class PhoneTests
    {
        private readonly Phone _phone;

        public PhoneTests()
        {
            _phone = new Phone("(15) 3331-2700");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(_phone.ToString(), "(15) 33312700");
        }

        [TestMethod()]
        public void ExtractAreaCodeTest()
        {
            Assert.AreEqual(_phone.ExtractAreaCode("(15) 3331-2700"), 15);
        }

        [TestMethod()]
        public void ExtractNumberTest()
        {
            Assert.AreEqual(_phone.ExtractNumber("(15) 3331-2700"), 33312700);
        }

        [TestMethod()]
        public void ClearPhoneNumberTest()
        {
            Assert.AreEqual(_phone.ClearPhoneNumber("(15) 3331-2700"), "1533312700");
        }
    }
}