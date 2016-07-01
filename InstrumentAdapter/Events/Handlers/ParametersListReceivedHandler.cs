using ApplicationServices;
using Infrastructure.DTOs;
using InstrumentCommunication.Sender;
using InstrumentCommunication.TsnAdapter.Events;

namespace InstrumentCommunication.Application.Events.Handlers
{
    public class ParametersListReceivedHandler:IHandler<ListOfParametersReceived>
    {
        private readonly IMessageSender messageSender;

        public ParametersListReceivedHandler(IMessageSender messageSender)
        {
            this.messageSender = messageSender;
        }

        public void Handle(ListOfParametersReceived domainCommand)
        {
            messageSender.SendMessage(new ParametersListMessageDTO(domainCommand.XmlMessage));
        }
    }
}