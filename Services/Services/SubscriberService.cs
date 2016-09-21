using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Repositories.Common;
using Repositories.DTOs;
using Services.Interfaces;

namespace Services.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly ISubscribersRepository _iSubscribersRepository;
        private readonly IMessageRepository _iMessageRepository;

        public SubscriberService(ISubscribersRepository iSubscribersRepository, IMessageRepository iMessageRepository)
        {
            _iSubscribersRepository = iSubscribersRepository;
            _iMessageRepository = iMessageRepository;
        }

        public async Task<List<KeyValue>> GetSubscribers()
        {
            var subscribers = await _iSubscribersRepository.GetAll();
            return subscribers.Select(s => new KeyValue { Key = s.StringId, Value = s.Name }).ToList();
        }

        public async Task<PullCriteria> GetSubscriberCriteria(string subscriberId)
        {
            var subscriber = await _iSubscribersRepository.Get(subscriberId);
            return subscriber.PullCriteria;
        }

        private IQueryable<Message> getQueryFilterByCriteria(IQueryable<Message> query,Subscriber subscriber)
        {
            var criteria = (subscriber.PullCriteria ?? new PullCriteria());

            var stringTags = criteria.Tags ?? "";
            var tags = stringTags.Split(',').ToList();

            if (!string.IsNullOrWhiteSpace(criteria.ContentContains))
                query = query.Where(w => w.Content.Contains(criteria.ContentContains));
            if (!string.IsNullOrWhiteSpace(criteria.TitleContains))
                query = query.Where(w => w.Title.Contains(criteria.TitleContains));
            if (!string.IsNullOrWhiteSpace(stringTags))
                query = query.Where(w => w.Tags.Intersect(tags).Any());

            return query;
        }

        public async Task<List<Message>> GetAllMessages(string subscriberId)
        {
            //get the subscriber
            var subscriber = await _iSubscribersRepository.Get(subscriberId);

            if(subscriber == null)
                throw new DomainException("There is no such a subscriber");

            //filter based on the subscriber criteria 
            return await Task.Run(() =>
            {
                var query = getQueryFilterByCriteria(_iMessageRepository.GetAllQueryable(), subscriber);
             
                return query.ToList();
            });
        }

        public async Task<List<Message>> GetNewMessages(string subscriberId)
        {
            var subscriber = await _iSubscribersRepository.Get(subscriberId);
            var pulledMessages = subscriber.PulledMessages ?? new List<Message>();
            var subscriberMessages = getQueryFilterByCriteria(_iMessageRepository.GetAllQueryable(), subscriber).ToList();

            //if the subscriber has no old messages(pulled messages) thats mean he needs all the messages that is matching the criteria
            var newMessages = (pulledMessages.Count == 0)? 
                subscriberMessages 
                : subscriberMessages.Except(pulledMessages).ToList();

            //Record that this subscriber get those messages to exclude them in the future
            if(subscriber.PulledMessages == null)
                subscriber.PulledMessages = subscriber.PulledMessages ?? new List<Message>();
            subscriber.PulledMessages.AddRange(newMessages);
            _iSubscribersRepository.Update(subscriber);

            //TODO: but how you gonna make sure this messages received by the client, you have to force the client to submit back the revived messages, then you can mark them as pulled
            return newMessages;
        }


        public async Task UpdateSubscriberCriteria(string subscriberId, string titleContains, string contentContains, string tags)
        {
            var subscriber = await _iSubscribersRepository.Get(subscriberId);

            subscriber.PullCriteria = subscriber.PullCriteria ?? new PullCriteria();
            subscriber.PullCriteria.TitleContains = titleContains;
            subscriber.PullCriteria.ContentContains = contentContains;
            subscriber.PullCriteria.Tags = tags;

            _iSubscribersRepository.Update(subscriber);
        }
    }
}