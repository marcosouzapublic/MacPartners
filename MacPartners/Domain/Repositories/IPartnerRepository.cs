using MacPartners.Domain.Interfaces;
using MacPartners.Domain.Models;

namespace MacPartners.Domain.Repositories
{
    public interface IPartnerRepository : IRepository<Partner>
    {
        void SaveChanges();
    }
}
