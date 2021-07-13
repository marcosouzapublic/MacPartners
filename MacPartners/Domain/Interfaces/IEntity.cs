using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacPartners.Domain.Interfaces
{
    public interface IEntity<T>
    {
        Guid Id { get; }

        void Create(IRepository<T> repository);
        void Update(IRepository<T> repository);
        void Delete(IRepository<T> repository);
    }
}
