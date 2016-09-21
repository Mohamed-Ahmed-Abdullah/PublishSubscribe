using System.Linq;
using Domain.Interfaces;
using Domain.Models;
using MongoDB.Driver.Linq;

namespace Repositories.Repositories
{
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        public MessageRepository() : base("messages"){}

        public IQueryable<Message> GetAllQueryable()
        {
            return GetCollection(Collection).AsQueryable();
        }
    }
}