using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Services.Interfaces
{
    public interface IMessageService
    {
        void SaveMessage(Message message);
        Task<List<Message>> GetMessages();
    }
}