using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Services.Interfaces;

namespace Services.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _iMessageRepository;

        public MessageService(IMessageRepository iMessageRepository)
        {
            _iMessageRepository = iMessageRepository;
        }

        public void SaveMessage(Message message)
        {
            _iMessageRepository.Save(message);
        }

        public async Task<List<Message>> GetMessages()
        {
            return await _iMessageRepository.GetAll();
        }
    }
}
