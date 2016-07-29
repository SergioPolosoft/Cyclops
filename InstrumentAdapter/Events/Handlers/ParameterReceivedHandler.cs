using ApplicationServices;
using Infrastructure.DTOs;
using InstrumentCommunication.Sender;
using InstrumentCommunication.TsnAdapter.Events;

namespace InstrumentCommunication.Application.Events.Handlers
{
    public class ParameterReceivedHandler:IHandler<ParameterReceived>
    {
        private readonly IMessageSender messageSender;

        public ParameterReceivedHandler(IMessageSender messageSender)
        {
            this.messageSender = messageSender;
        }

        public void Handle(ParameterReceived domainCommand)
        {
            messageSender.SendMessage(new ParameterDTO(domainCommand.Document));
        }
    }
}