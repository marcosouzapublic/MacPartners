using MacPartners.Domain.Models.Entities;
using MacPartners.Domain.Repositories;
using MacPartners.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MacPartners.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MacPartnersContext _context;

        public UserRepository(MacPartnersContext context)
        {
            _context = context;
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public User Find(Guid id)
        {
            return _context.Users.Find(id);
        }

        public IList<User> ToList()
        {
            return _context.Users.ToList();
        }

        public IList<User> ToList(Expression<Func<User, bool>> expression)
        {
            return _context.Users.Where(expression).ToList();
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public User Find(string email, string password)
        {
            return _context.Users.Where(u => u.Person.Email.EmailAdress == email && u.Password == password).FirstOrDefault();
        }

        public User Find(string email)
        {
            return _context.Users.Where(u => u.Person.Email.EmailAdress == email).FirstOrDefault();
        }
    }
}
