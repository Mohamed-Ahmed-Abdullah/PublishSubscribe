using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Repositories.DTOs;
using Services.Interfaces;

namespace Services.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublishersRepository _iPublishersRepository;
        private readonly IMessageRepository _iMessageRepository;
        public PublisherService(IPublishersRepository iPublishersRepository, IMessageRepository iMessageRepository)
        {
            _iPublishersRepository = iPublishersRepository;
            _iMessageRepository = iMessageRepository;
        }

        public async Task<List<KeyValue>> GetPublishers()
        {
            var publishers = await _iPublishersRepository.GetAll();
            return publishers.Select(s => new KeyValue {Key = s.StringId, Value = s.Name}).ToList();
        }

        public async Task DistributeMessage(string publisherId,string title, string message, string tags)
        {
            //save message
            var messageId = _iMessageRepository.Save(new Message
            {
                Title = title,
                Content = message,
                Tags = (tags+"").Split(',').ToList()
            });

            //Update publisher
            var publisher = await _iPublishersRepository.Get(publisherId);

            if(publisher.PushedMessages == null)
                publisher.PushedMessages = new List<Message>();

            publisher.PushedMessages.Add( await _iMessageRepository.Get(messageId));

            _iPublishersRepository.Update(publisher);
        }
    }
}