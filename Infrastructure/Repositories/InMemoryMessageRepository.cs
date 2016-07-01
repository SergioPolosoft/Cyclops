using System.Collections.Generic;
using InstrumentAdapter.Domain;

namespace Infrastructure.Repositories
{
    public class InMemoryMessageRepository : IMessagesRepository
    {
        private readonly IList<Message> storedMessages = new List<Message>();

        public void Save(Message message)
        {
            this.storedMessages.Add(message);
        }
    }
}