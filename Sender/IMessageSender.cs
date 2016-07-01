using Infrastructure.DTOs;

namespace InstrumentCommunication.Sender
{
    public interface IMessageSender
    {
        void SendMessage(MessageDTO messageDto);
    }
}