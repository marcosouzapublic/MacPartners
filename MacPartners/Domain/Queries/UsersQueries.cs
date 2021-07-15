using MacPartners.Domain.Models.Entities;
using MacPartners.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacPartners.Domain.Queries
{
    public class UsersQueries
    {
        private readonly IUserRepository _repository;

        public UsersQueries(IUserRepository repository)
        {
            _repository = repository;
        }

        public IList<User> AllUsers()
        {
            return _repository.ToList();
        }

        public IList<User> BlockedUsers()
        {
            return _repository.ToList(u => u.IsBlocked);
        }

        public IList<User> UnblockedUsers()
        {
            return _repository.ToList(u => !u.IsBlocked);
        }
    }
}
