using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models.Entities;
using MacPartners.Domain.Models.Enums;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Repositories;
using System.Linq;
using System.Security.Principal;

namespace MacPartners.Infra.Services
{
    public static class UserService
    {
       public static void CheckSuperUser(IUserRepository userRepository, ICrypter crypter)
        {
            var superUser = userRepository.Find("contato@manchesterautomacao.com.br");
            if (superUser == null)
                CreateSuperUser(userRepository, crypter);
        }

        private static void CreateSuperUser(IUserRepository repository, ICrypter crypter)
        {
            var email = new Email("contato@manchesterautomacao.com.br");
            var superUser = new User("Swed@2021", new Person("Manchester", "SuperUser", new Cpf("41598913816"), null, email, new Phone("(15) 3331-2700")), crypter, EUserRole.Administrator);
            superUser.Create(repository);
            repository.SaveChanges();
        }
    }
}
