using ApplicationServices;
using LabConfiguration.Application.Exceptions;
using LabConfiguration.Domain;

namespace LabConfiguration.Application.Commands.Handlers
{
    public class ConfirmApplicationDeletionHandler:IHandler<ConfirmApplicationDeletionCommand,IResponse>
    {
        private readonly IApplicationRepository applicationRepository;

        public ConfirmApplicationDeletionHandler(IApplicationRepository applicationRepository)
        {
            this.applicationRepository = applicationRepository;            
        }

        public IResponse Handle(ConfirmApplicationDeletionCommand command)
        {
            if (applicationRepository.ApplicationCodeExists(command.ApplicationCode))
            {
                applicationRepository.RemoveByApplicationCode(command.ApplicationCode);
            }
            else
            {
                throw new ApplicationNotExistException();
            }

            return new CommandSuccesfullyHandled();
        }
    }
}