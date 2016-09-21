using System.Linq;
using Domain.Models;

namespace Domain.Interfaces
{   
    public interface IMessageRepository : IGenericInterface<Message>
    {
        IQueryable<Message> GetAllQueryable();
    }
}