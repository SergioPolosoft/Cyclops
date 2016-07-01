using System;
using ApplicationServices;
using InstrumentAdapter.Domain;

namespace InstrumentCommunication.Application.Commands.Handlers
{
    public class StoreMessageHandler:IHandler<ActionCommand,IResponse>
    {
        private readonly IMessagesRepository messagesRepository;
        private readonly Message messageToStore;

        public StoreMessageHandler(IMessagesRepository messagesRepository, ICommandWithMessage commandWithMessage)
        {
            this.messagesRepository = messagesRepository;
            this.messageToStore = commandWithMessage.Message;
        }

        public IResponse Handle(ActionCommand command)
        {
            this.messagesRepository.Save(messageToStore);
            
            command.InvokeAction();

            return new CommandSuccesfullyHandled();
        }
    }
}