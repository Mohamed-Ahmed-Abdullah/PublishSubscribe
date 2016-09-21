using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Repositories.Common;
using Assert = NUnit.Framework.Assert;

namespace PubSubUnitTest
{
    [TestFixture]
    public class SubscribersService
    {
        [Test]
        public async Task When_GetSubscribers_ThenGetAllShouldBeCalled()
        {
            //Arrange
            var moqSubscriberRepository = new Mock<ISubscribersRepository>(MockBehavior.Strict);
            var moqMessageRepository = new Mock<IMessageRepository>(MockBehavior.Strict);
            var subscriberService = new Services.Services.SubscriberService(moqSubscriberRepository.Object,moqMessageRepository.Object);

            moqSubscriberRepository.Setup(x => x.GetAll())
                .Returns(Task.FromResult(new List<Subscriber>()));

            //Act
            await subscriberService.GetSubscribers();

            //Assert 
            moqSubscriberRepository.Verify(x => x.GetAll(), Times.Exactly(1));
        }

        [Test]
        public async Task When_UpdateSubscriberCriteria_GetAndUpdateShouldBeCalled()
        {
            //Arrange
            var moqSubscriberRepository = new Mock<ISubscribersRepository>(MockBehavior.Strict);
            var moqMessageRepository = new Mock<IMessageRepository>(MockBehavior.Strict);
            var subscriberService = new Services.Services.SubscriberService(moqSubscriberRepository.Object,moqMessageRepository.Object);

            moqSubscriberRepository.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(Task.FromResult(new Subscriber()));
            moqSubscriberRepository.Setup(x => x.Update(It.IsAny<Subscriber>()));
            //Act
            await subscriberService.UpdateSubscriberCriteria(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert 
            moqSubscriberRepository.Verify(x => x.Get(It.IsAny<string>()), Times.Exactly(1));
            moqSubscriberRepository.Verify(x => x.Update(It.IsAny<Subscriber>()), Times.Exactly(1));
        }

        [Test]
        public async Task When_GetSubscriberCriteria_GetShouldBeCalled()
        {
            //Arrange
            var moqSubscriberRepository = new Mock<ISubscribersRepository>(MockBehavior.Strict);
            var moqMessageRepository = new Mock<IMessageRepository>(MockBehavior.Strict);
            var subscriberService = new Services.Services.SubscriberService(moqSubscriberRepository.Object,moqMessageRepository.Object);

            moqSubscriberRepository.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(Task.FromResult(new Subscriber()));

            //Act
            await subscriberService.GetSubscriberCriteria(It.IsAny<string>());

            //Assert 
            moqSubscriberRepository.Verify(x => x.Get(It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public async Task When_GetAllMessages_GetAllAndFilterShouldBeCalled()
        {
            //Arrange
            var moqSubscriberRepository = new Mock<ISubscribersRepository>(MockBehavior.Strict);
            var moqMessageRepository = new Mock<IMessageRepository>(MockBehavior.Strict);
            var subscriberService = new Services.Services.SubscriberService(moqSubscriberRepository.Object,moqMessageRepository.Object);

            moqSubscriberRepository.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(Task.FromResult(new Subscriber()));
            moqMessageRepository.Setup(x => x.GetAllQueryable())
                .Returns(new List<Message>{ new Message() , new Message() }.AsQueryable());

            //Act
            var messages = await subscriberService.GetAllMessages(It.IsAny<string>());

            //Assert 
            moqSubscriberRepository.Verify(x => x.Get(It.IsAny<string>()), Times.Exactly(1));
            Assert.AreEqual(messages.Count, 2, "Messages Count Should be the same");
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException))]
        public async Task When_PassingNullToGetAllMessages_GetAllAndFilterShouldBeCalled()
        {
            //Arrange
            var moqSubscriberRepository = new Mock<ISubscribersRepository>(MockBehavior.Strict);
            var moqMessageRepository = new Mock<IMessageRepository>(MockBehavior.Strict);
            var subscriberService = new Services.Services.SubscriberService(moqSubscriberRepository.Object, moqMessageRepository.Object);

            moqSubscriberRepository.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(Task.FromResult<Subscriber>(null));
            moqMessageRepository.Setup(x => x.GetAllQueryable())
                .Returns(new List<Message> { }.AsQueryable());

            //Act
            await subscriberService.GetAllMessages(It.IsAny<string>());

            //Assert 
        }

        //GetNewMessages
    }
}