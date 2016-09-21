using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericInterface<T>
    {
        string Save(T entity);
        void Update(T entity);
        Task<List<T>> GetAll();
        Task<T> Get(string id);
    }
}