using ApplicationServices;
using InstrumentAdapter.Domain;

namespace InstrumentCommunication.Application.Commands.Handlers
{
    public class ConfirmMessageReceptionHandler:IHandler<ConfirmMessageReceptionCommand, IResponse>
    {
        private readonly ICommunicationStatusRepository communicationStatusRepository;

        public ConfirmMessageReceptionHandler(ICommunicationStatusRepository communicationStatusRepository)
        {
            this.communicationStatusRepository = communicationStatusRepository;
        }

        public IResponse Handle(ConfirmMessageReceptionCommand command)
        {
            communicationStatusRepository.WaitForMessages();
            return new CommandSuccesfullyHandled();
        }
    }
}