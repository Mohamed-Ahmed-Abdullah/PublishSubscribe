using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Repositories.DTOs;

namespace Services.Interfaces
{
    public interface ISubscriberService
    {
        Task<List<KeyValue>> GetSubscribers();
        Task UpdateSubscriberCriteria(string subscriberId, string titleContains, string contentContains, string tags);
        Task<PullCriteria> GetSubscriberCriteria(string subscriberId);

        Task<List<Message>> GetAllMessages(string subscriberId);
        Task<List<Message>> GetNewMessages(string subscriberId);
    }
}