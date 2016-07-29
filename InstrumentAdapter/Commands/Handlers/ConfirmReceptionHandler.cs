using ApplicationServices;
using Infrastructure.DTOs;
using InstrumentCommunication.Sender;

namespace InstrumentCommunication.Application.Commands.Handlers
{
    public class ConfirmReceptionHandler : IHandler<ActionCommand,IResponse>
    {
        private readonly IMessageSender messageQueue;

        public ConfirmReceptionHandler(IMessageSender messageQueue)
        {
            this.messageQueue = messageQueue;
        }

        public IResponse Handle(ActionCommand command)
        {
            command.InvokeAction();

            messageQueue.SendMessage(new ConfirmationMessageDTO());

            return new CommandSuccesfullyHandled();
        }
    }
}