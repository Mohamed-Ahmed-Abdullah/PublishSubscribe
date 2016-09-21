using System.Collections.Generic;
using System.Threading.Tasks;
using Repositories.DTOs;

namespace Services.Interfaces
{
    public interface IPublisherService
    {
        Task<List<KeyValue>> GetPublishers();
        Task DistributeMessage(string publisherId, string title, string message, string tags);
    }
}