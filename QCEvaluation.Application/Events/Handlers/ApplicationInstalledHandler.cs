using System;
using ApplicationServices;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Application.Events.Handlers
{
    public class ApplicationInstalledHandler:IHandler<ApplicationInstalled>
    {
        private readonly IQCApplicationRepository applicationRepository;

        public ApplicationInstalledHandler(IQCApplicationRepository applicationRepository)
        {
            this.applicationRepository = applicationRepository;
        }

        public void Handle(ApplicationInstalled domainCommand)
        {
            var applicationQC = applicationRepository.GetApplicationByTestCode(domainCommand.TestCode) ??
                                new ApplicationQC();

            applicationQC.Create(Guid.NewGuid(), domainCommand.TestCode);

            applicationRepository.Save(applicationQC.Changes);
        }
    }
}