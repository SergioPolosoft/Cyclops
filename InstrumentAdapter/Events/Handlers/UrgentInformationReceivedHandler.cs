using Infrastructure.DTOs;
using InstrumentCommunication.Sender;
using InstrumentCommunication.TsnAdapter.Events;

namespace InstrumentCommunication.Application.Events.Handlers
{
    public class UrgentInformationReceivedHandler
    {
        private readonly IMessageSender messageSender;

        public UrgentInformationReceivedHandler(IMessageSender messageSender)
        {
            this.messageSender = messageSender;
        }

        public void Handle(UrgentInformationReceived urgentInformationReceived)
        {
            messageSender.SendMessage(new UrgentInformationDownloadDTO(urgentInformationReceived.XmlDocument));
        }
    }
}