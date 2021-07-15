using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models.Entities;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Repositories;
using System;

namespace MacPartners.Domain.Commands
{
    public class CreateUserCommand
    {
        private readonly IUserRepository _repository;
        private readonly ICrypter _crypter;

        public CreateUserCommand(IUserRepository repository, ICrypter crypter)
        {
            _repository = repository;
            _crypter = crypter;
        }

        public TransactionResponse<User> CreateUser(Person person)
        {
            try
            {
                var user = new User(person.Email.EmailAdress, person, _crypter);
                user.Create(_repository);
                _repository.SaveChanges();

                return new Models.ValueObjects.TransactionResponse<User>(user);
            }
            catch (Exception exception)
            {
                return new Models.ValueObjects.TransactionResponse<User>(exception);
            }
        }
    }
}
