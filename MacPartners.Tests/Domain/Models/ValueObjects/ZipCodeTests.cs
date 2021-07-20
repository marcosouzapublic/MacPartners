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
    public class ZipCodeTests
    {
        [TestMethod()]
        public void ValidTestValidCase()
        {
            var number = "18078-180";
            var cep = new ZipCode(number);

            Assert.IsTrue(cep.Valid());
        }

        [TestMethod()]
        public void ValidTestInvalidCase()
        {
            var number = "180350-0000";
            var cep = new ZipCode(number);

            Assert.IsFalse(cep.Valid());
        }
    }
}