using ApplicationServices;
using LabConfiguration.Application.Exceptions;
using LabConfiguration.Domain;

namespace LabConfiguration.Application.Commands.Handlers
{
    public class ConfirmApplicationInstallationHandler:IHandler<ConfirmApplicationInstallationCommand,IResponse>
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly IQCEvaluationPort qcevaluationServices;

        public ConfirmApplicationInstallationHandler(IApplicationRepository applicationRepository, IQCEvaluationPort qcevaluationServices)
        {
            this.applicationRepository = applicationRepository;
            this.qcevaluationServices = qcevaluationServices;
        }

        public IResponse Handle(ConfirmApplicationInstallationCommand command)
        {
            var applicationToInstall = command.ApplicationDTO;

            var applicationCode = applicationToInstall.TestCode;

            if (applicationRepository.ApplicationCodeExists(applicationCode))
            {
                throw new ApplicationExistException();
            }

            applicationRepository.Add(new ApplicationTest(applicationCode));

            qcevaluationServices.NotifyApplicationInstalled(applicationCode);

            return new CommandSuccesfullyHandled();
        }
    }
}