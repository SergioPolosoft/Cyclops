using System;
using Infrastructure.DTOs;

namespace Infrastructure.MessageQueue
{
    public class MessageQueue : IMessageQueue
    {
        private static IMessageQueue instance;

        public static IMessageQueue Instance
        {
            get { return instance ?? (instance = new MessageQueue()); }
        }

        public void SendMessage(MessageDTO message)
        {
            if (MessageReceived!=null)
            {
                MessageReceived(message);    
            }
        }

        public event Action<MessageDTO> MessageReceived;
    }
}