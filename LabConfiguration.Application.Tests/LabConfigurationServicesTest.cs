using LabConfiguration.Application.Commands;
using LabConfiguration.Application.Exceptions;
using LabConfiguration.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LabConfiguration.Application.Tests
{
    [TestClass]
    public class LabConfigurationServicesTest
    {
        private LabConfigurationService labConfigurationServices;
        private Mock<IApplicationRepository> applicationRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            var configurationRepository = new Mock<IConfigurationRepository>();
            applicationRepository = new Mock<IApplicationRepository>();
            labConfigurationServices = new LabConfigurationService(configurationRepository.Object, applicationRepository.Object, new Mock<IQCEvaluationPort>().Object);
        }

        [TestMethod]
        public void WhenAnApplicationIsConfirmed_ItIsInstalledOnTheSystem()
        {
            int application = 1237;

            labConfigurationServices.Handle(new ConfirmApplicationInstallationCommand(new ApplicationDTO(application)));

            applicationRepository.Verify(x=>x.Add(It.IsAny<Domain.ApplicationTest>()));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationExistException))]
        public void WhenAnApplicationIsConfirmaed_IfItExistAnExceptionIsThrown()
        {
            applicationRepository.Setup(x => x.ApplicationCodeExists(1435)).Returns(true).Verifiable();

            labConfigurationServices.Handle(new ConfirmApplicationInstallationCommand(new ApplicationDTO(1435)));                       
        }

        [TestMethod]
        public void WhenAnApplicationIsDeleted_ItIsDeactivatedFromTheSystem()
        {
            applicationRepository.Setup(x => x.ApplicationCodeExists(1437)).Returns(true);

            labConfigurationServices.Handle(new ConfirmApplicationDeletionCommand(1437));

            applicationRepository.Verify(x => x.RemoveByApplicationCode(1437));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationNotExistException))]
        public void WhenAnApplicationIsDeleted_IfTheApplicationDoesNotExistAnExceptionIsThrown()
        {
            applicationRepository.Setup(x => x.ApplicationCodeExists(1439)).Returns(false);

            labConfigurationServices.Handle(new ConfirmApplicationDeletionCommand(1439));
        }

        [TestMethod]
        public void WhenAnApplicationIsUpdated_ItIsUpdatedOnTheSystem()
        {
            applicationRepository.Setup(x => x.ApplicationCodeExists(1439)).Returns(true).Verifiable();

            labConfigurationServices.Handle(new ConfirmApplicationUpdateCommand(1439));

            applicationRepository.Verify(x => x.Update(It.IsAny<Domain.ApplicationTest>()));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationNotExistException))]
        public void WhenAnApplicationIsUpdated_IfTheApplicationDoesNotExistAnExceptionIsThrown()
        {
            applicationRepository.Setup(x => x.ApplicationCodeExists(1440)).Returns(false).Verifiable();

            labConfigurationServices.Handle(new ConfirmApplicationUpdateCommand(1440));                       
        }
        
    }
}
