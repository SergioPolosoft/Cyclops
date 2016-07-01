using System;
using ApplicationServices;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Application.Commands.Handlers
{
    public class DisableRuleForApplicationHandler:IHandler<DisableRuleForApplication, IResponse>
    {
        private readonly IQCApplicationRepository applicationsRepository;

        public DisableRuleForApplicationHandler(IQCApplicationRepository applicationsRepository)
        {
            this.applicationsRepository = applicationsRepository;
        }

        public IResponse Handle(DisableRuleForApplication domainCommand)
        {
            var application = applicationsRepository.GetApplication(domainCommand.ApplicationId);

            if (application==null)
            {
                throw new InvalidOperationException(string.Format("The application {0} does not exists.",domainCommand.ApplicationId));
            }

            application.DisableRule(domainCommand.Rule);

            applicationsRepository.Save(application.Changes);

            return new CommandSuccesfullyHandled();
        }
    }
}