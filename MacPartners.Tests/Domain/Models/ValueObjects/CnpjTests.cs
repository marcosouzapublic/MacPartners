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
    public class CnpjTests
    {
        [TestMethod()]
        public void ValidTestValidCase()
        {
            var number = "05.061.448/0001-39";
            var cnpj = new Cnpj(number);

            Assert.IsTrue(cnpj.Valid());
        }

        [TestMethod()]
        public void ValidTestInvalidCase()
        {
            var number = "05.061.448/0001-35";
            var cnpj = new Cnpj(number);

            Assert.IsFalse(cnpj.Valid());
        }
    }
}