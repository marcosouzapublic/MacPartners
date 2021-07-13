using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MacPartners.Domain.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T newObject);
        void Update(T updatedObject);
        void Delete(T toRemoveObject);
        T Find(Guid id);
        IList<T> ToList();
        IList<T> ToList(Expression<Func<T, bool>> expression);
    }
}
