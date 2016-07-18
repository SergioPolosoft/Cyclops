using ApplicationServices;
using LabConfiguration.Application.Exceptions;
using LabConfiguration.Domain;

namespace LabConfiguration.Application.Commands.Handlers
{
    public class ConfirmApplicationUpdateHandler : IHandler<ConfirmApplicationUpdateCommand,IResponse>
    {
        private readonly IApplicationRepository applicationRepository;

        public ConfirmApplicationUpdateHandler(IApplicationRepository applicationRepository)
        {
            this.applicationRepository = applicationRepository;
        }

        public IResponse Handle(ConfirmApplicationUpdateCommand command)
        {
            if (applicationRepository.ApplicationCodeExists(command.ApplicationCode))
            {

                this.applicationRepository.Update(command.ApplicationTest);    
            }
            else
            {
                throw new ApplicationNotExistException();
            }
            return new CommandSuccesfullyHandled();
        }
    }
}