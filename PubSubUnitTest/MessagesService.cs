using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using NUnit.Framework;

namespace PubSubUnitTest
{
    [TestFixture]
    public class MessagesService
    {
        [Test]
        public void WhenSave_MethodShouldBeCalled()
        {
            //Arrange
            var moqMessageRepository = new Mock<IMessageRepository>(MockBehavior.Strict);
            var messageService = new Services.Services.MessageService(moqMessageRepository.Object);
            moqMessageRepository.Setup(x => x.Save(It.IsAny<Message>())).Returns(It.IsAny<string>());

            //Act
            messageService.SaveMessage(It.IsAny<Message>());

            //Assert 
            moqMessageRepository.Verify(x => x.Save(It.IsAny<Message>()), Times.Exactly(1));
        }

        [Test]
        public async Task WhenGetMessages_GetShouldBeInvoked()
        {
            //Arrange
            var moqMessageRepository = new Mock<IMessageRepository>(MockBehavior.Strict);
            var messageService = new Services.Services.MessageService(moqMessageRepository.Object);

            //Act
            await messageService.GetMessages();

            //Assert 
            moqMessageRepository.Verify(x => x.GetAll(), Times.Exactly(1));
        }
    }
}
