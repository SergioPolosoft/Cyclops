using ApplicationServices;
using LabConfiguration.Application.Exceptions;
using LabConfiguration.Domain;
using QCEvaluation.Application;
using QCEvaluation.Application.Events;

namespace LabConfiguration.Application.Commands.Handlers
{
    public class ConfirmApplicationInstallationHandler:IHandler<ConfirmApplicationInstallationCommand,IResponse>
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly IQCEvaluationServices qcevaluationServices;

        public ConfirmApplicationInstallationHandler(IApplicationRepository applicationRepository, IQCEvaluationServices qcevaluationServices)
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

            applicationRepository.Add(new Domain.Application(applicationCode));

            qcevaluationServices.Handle(new ApplicationInstalled(applicationCode));

            return new CommandSuccesfullyHandled();
        }
    }
}