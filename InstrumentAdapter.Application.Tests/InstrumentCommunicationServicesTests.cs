using System.Xml;
using Infrastructure.DTOs;
using InstrumentAdapter.Domain;
using InstrumentCommunication.Application;
using InstrumentCommunication.Application.Commands;
using InstrumentCommunication.Application.Events;
using InstrumentCommunication.Sender;
using InstrumentCommunication.TsnAdapter;
using InstrumentCommunication.TsnAdapter.Commands;
using InstrumentCommunication.TsnAdapter.Events;
using LabConfiguration.Application;
using LabConfiguration.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCEvaluation.Application;
using QCRoutine.Application;

namespace InstrumentAdapter.Application.Tests
{
    [TestClass]
    public class InstrumentCommunicationServicesTests
    {
        private InstrumentCommunicationServices applicationService;
        private Mock<IMessagesRepository> messageRepository;
        private Mock<IMessageSender> messageSender;
        private Mock<ITSNAdapter> tsnAdapter;

        [TestInitialize]
        public void TestInitialize()
        {
            Mock<IConfigurationRepository> configurationRepository = new Mock<IConfigurationRepository>();

            Mock<ICommunicationStatusRepository> communicationStatusRepository = new Mock<ICommunicationStatusRepository>();
            messageRepository = new Mock<IMessagesRepository>();

            messageSender =  new Mock<IMessageSender>();
            tsnAdapter = new Mock<ITSNAdapter>();
            Mock<IApplicationRepository> applicationRepository = new Mock<IApplicationRepository>();
            ILabConfigurationServices labConfigurationServices = new LabConfigurationService(configurationRepository.Object, applicationRepository.Object, new Mock<IQCEvaluationServices>().Object);
            applicationService = new InstrumentCommunicationServices(communicationStatusRepository.Object, tsnAdapter.Object, messageSender.Object, messageRepository.Object, new LabConfigurationAdapter.LabConfigurationAdapter(labConfigurationServices),new Mock<IQCRoutineServices>().Object);
        }

        [TestMethod]
        public void WhenUrgentInformationRequestedTsnAdapterIsCalled()
        {
            applicationService.Handle(new UrgentInformationRequestCommand(new UrgentInformationMessage()));

            tsnAdapter.Verify(x=>x.Handle(It.IsAny<DownloadUrgentInformation>()),Times.Once);
        }

        [TestMethod]
        public void WhenUrgentInformationRequestedConfirmationIsSentAfterEventCreated()
        {
            applicationService.Handle(new UrgentInformationRequestCommand(new UrgentInformationMessage()));

            messageSender.Verify(x => x.SendMessage(It.IsAny<ConfirmationMessageDTO>()), Times.Once);
        }

        [TestMethod]
        public void WhenUrgentInformationEventIsCreatedTheUrgentInformationMessageIsStored()
        {
            UrgentInformationMessage urgentInformationMessage = new UrgentInformationMessage();
            applicationService.Handle(new UrgentInformationRequestCommand(urgentInformationMessage));

            messageRepository.Verify(x => x.Save(urgentInformationMessage));
        }

        [TestMethod]
        public void WhenUrgentInformationIsReceivedFromTSNAgentAMessageIsSent()
        {
           tsnAdapter.Raise(x=>x.Publish+=null, new UrgentInformationReceived(new XmlDocument()));

           messageSender.Verify(x => x.SendMessage(It.IsAny<UrgentInformationDownloadDTO>()), Times.Once);
        }      
    }
}