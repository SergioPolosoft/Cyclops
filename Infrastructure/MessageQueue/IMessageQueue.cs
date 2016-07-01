using System;
using Infrastructure.DTOs;

namespace Infrastructure.MessageQueue
{
    public interface IMessageQueue
    {
        void SendMessage(MessageDTO message);
        event Action<MessageDTO> MessageReceived;
    }
}