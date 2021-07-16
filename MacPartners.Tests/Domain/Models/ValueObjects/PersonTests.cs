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
    public class PersonTests
    {
        private readonly Person _person;

        public PersonTests()
        {
            _person = new Person("Bruce", "Wayne", new Cpf("349.655.750-83"), null, new Email("batman@dc.com"), new Phone("(15) 3333-3333"));
        }

        [TestMethod()]
        public void ChangeNameTest()
        {
            _person.ChangeName("Robin");

            Assert.AreEqual(_person.Name, "Robin");
        }

        [TestMethod()]
        public void ChangeLastNameTest()
        {
            _person.ChangeLastName("Todd");

            Assert.AreEqual(_person.LastName, "Todd");
        }

        [TestMethod()]
        public void ChangeBirthdayTest()
        {
            _person.ChangeBirthday(DateTime.Today);

            Assert.AreEqual(_person.Birthday, DateTime.Today);
        }

        [TestMethod()]
        public void ChangePhoneTest()
        {
            _person.ChangePhone("(15) 3331-2700");

            Assert.AreEqual(_person.Phone.ToString(), "(15) 3331-2700");
        }
    }
}