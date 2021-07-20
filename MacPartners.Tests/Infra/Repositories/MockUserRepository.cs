using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models.Entities;
using MacPartners.Domain.Models.Enums;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Repositories;
using MacPartners.Infra.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MacPartners.Tests.Infra.Repositories
{
    public class MockUserRepository : IUserRepository
    {
        private IList<User> _users;
        private ICrypter _crypter;

        public MockUserRepository()
        {
            _crypter = new CrypterService();

            _users = new List<User>();
            _users.Add(new User("123456", new Person("Bruce", "Wayne", new Cpf("05839583898"), null, new Email("batman@justiceleague.com"), new Phone("(15) 9911-11111")), _crypter, EUserRole.Partner));
            _users.Add(new User("789456123", new Person("Clark", "Kent", new Cpf("41598913816"), null, new Email("superman@justiceleague.com"), new Phone("(15) 9911-11111")), _crypter, EUserRole.Partner));
        }

        public void Create(User user)
        {
            _users.Add(user);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }

        public User Find(Guid id)
        {
            return _users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User Find(string email, string password)
        {
            return _users.Where(u => u.Person.Email.EmailAdress == email && u.Password == password).FirstOrDefault();
        }

        public User Find(string email)
        {
            return _users.Where(u => u.Person.Email.EmailAdress == email).FirstOrDefault();
        }

        public void SaveChanges()
        {
            
        }

        public IList<User> ToList()
        {
            return _users;
        }

        public IList<User> ToList(Expression<Func<User, bool>> expression)
        {
            return _users;
        }

        public void Update(User user)
        {
            Delete(user);
            Create(user);
        }
    }
}
