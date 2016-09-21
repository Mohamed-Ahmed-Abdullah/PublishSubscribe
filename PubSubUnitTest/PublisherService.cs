using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using NUnit.Framework;

namespace PubSubUnitTest
{
    [TestFixture]
    public class PublisherService
    {
        [Test]
        public async Task WhenGetPublishers_RepositoryGetShouldBeCalled()
        {
            //Arrange
            var moqPublishRepository = new Mock<IPublishersRepository>(MockBehavior.Strict);
            var moqMessageRepository = new Mock<IMessageRepository>(MockBehavior.Strict);
            var publisherService = new Services.Services.PublisherService(moqPublishRepository.Object, moqMessageRepository.Object);

            moqPublishRepository.Setup(x => x.GetAll())
                .Returns(Task.FromResult(new List<Publisher>()));

            //Act
            await publisherService.GetPublishers();

            //Assert 
            moqPublishRepository.Verify(x => x.GetAll(), Times.Exactly(1));
        }

        [Test]
        public async Task WhenDistributeMessage_SaveMessageAndUpdatePublisherShouldBeCalled()
        {
            //Arrange
            var moqPublishRepository = new Mock<IPublishersRepository>(MockBehavior.Strict);
            var moqMessageRepository = new Mock<IMessageRepository>(MockBehavior.Strict);
            var publisherService = new Services.Services.PublisherService(moqPublishRepository.Object, moqMessageRepository.Object);

            moqPublishRepository.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(Task.FromResult(new Publisher()));
            moqPublishRepository.Setup(x => x.Update(It.IsAny<Publisher>()));
            moqMessageRepository.Setup(x => x.Save(It.IsAny<Message>()))
                .Returns(It.IsAny<string>());
            moqMessageRepository.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(Task.FromResult(new Message()));

            //Act
            await publisherService.DistributeMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert 
            moqPublishRepository.Verify(x => x.Update(It.IsAny<Publisher>()), Times.Exactly(1));
            moqMessageRepository.Verify(x=>x.Save(It.IsAny<Message>()),Times.Exactly(1));
        }
    }
}