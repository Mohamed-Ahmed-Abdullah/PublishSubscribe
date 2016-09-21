using Domain.Interfaces;
using Domain.Models;

namespace Repositories.Repositories
{
    public class SubscribersRepository : RepositoryBase<Subscriber>, ISubscribersRepository
    {
        public SubscribersRepository():base("subscribers")
        {
        }
    }
}