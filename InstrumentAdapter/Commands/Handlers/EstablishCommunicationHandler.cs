using ApplicationServices;
using Infrastructure.DTOs;
using InstrumentAdapter.Domain;
using InstrumentCommunication.Application.Ports;
using InstrumentCommunication.Sender;

namespace InstrumentCommunication.Application.Commands.Handlers
{
    public class EstablishCommunicationHandler:IHandler<EstablishCommunicationCommand,IResponse>
    {
        private readonly ICommunicationStatusRepository communicationStatusRepository;
        private readonly IMessageSender messageSender;
        private readonly ILabConfigurationPort labConfigurationServices;

        public EstablishCommunicationHandler(ICommunicationStatusRepository communicationStatusRepository, IMessageSender messageSender, ILabConfigurationPort labConfigurationServices)
        {
            this.communicationStatusRepository = communicationStatusRepository;
            this.messageSender = messageSender;
            this.labConfigurationServices = labConfigurationServices;
        }

        public IResponse Handle(EstablishCommunicationCommand command)
        {
            var ftpConfiguration = labConfigurationServices.GetFTPConfiguration();

            var ftpCredentialsMessage = new FTPCredentialsMessageDTO
            {
                User = ftpConfiguration.User,
                Password = ftpConfiguration.Password,
                DataBasket = ftpConfiguration.DataBasket
            };

            this.communicationStatusRepository.WaitForAcknowledge();

            messageSender.SendMessage(ftpCredentialsMessage);

            return new CommandSuccesfullyHandled();
        }
    }
}