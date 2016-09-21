using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Domain.Models;
using Repositories.DTOs;
using Services.Interfaces;

namespace PublishSubscribe.Controllers
{
    public class PubSubController : ApiController
    {
        private readonly IMessageService _iMessageService;
        private readonly IPublisherService _iPublisherService;
        private readonly ISubscriberService _iSubscriberService;
        public PubSubController(IMessageService iMessageService, 
                                IPublisherService iPublisherService,
                                ISubscriberService iSubscriberService)
        {
            _iMessageService = iMessageService;
            _iPublisherService = iPublisherService;
            _iSubscriberService = iSubscriberService;
        }

        [HttpGet]
        public async Task<List<Message>> Get()
        {
            return await _iMessageService.GetMessages();
        }

        [HttpGet]
        public async Task<List<KeyValue>> GetPublishers()
        {
            return await _iPublisherService.GetPublishers();
        }

        [HttpPost]
        public void DistributeMessage(string publisherId, string title, string message, string tags)
        {
            //TODO: don't send the message throw the URL since it has only 2000 char and it's not a good practice
            _iPublisherService.DistributeMessage(publisherId,title,message,tags);
        }

        [HttpGet]
        public async Task<List<KeyValue>> GetSubscribers()
        {
            return await _iSubscriberService.GetSubscribers();
        }

        [HttpPost]
        public async void UpdateSubscriberCriteria(string subscriberId,string titleContains, string contentContains,string tags)
        {
            await _iSubscriberService.UpdateSubscriberCriteria(subscriberId, titleContains, contentContains, tags);
        }

        [HttpGet]
        public async Task<List<Message>> GetAllMessages(string subscriberId)
        {            
            return await _iSubscriberService.GetAllMessages(subscriberId);
        }

        [HttpGet]
        public async Task<PullCriteria> GetSubscriberCriteria(string subscriberId)
        {
            return await _iSubscriberService.GetSubscriberCriteria(subscriberId);
        }

        [HttpGet]
        public async Task<List<Message>> GetAllMessages()
        {
            return await _iMessageService.GetMessages();
        }
    }
}