using ApplicationServices;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Exceptions;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Application.Commands.Handlers
{
    public class EnableRuleForApplicationHandler:IHandler<EnableRuleForApplication, IResponse>
    {
        private readonly IQCApplicationRepository applicationRepository;
        private readonly IQCRuleRepository qcRuleRepository;

        public EnableRuleForApplicationHandler(IQCApplicationRepository applicationRepository, IQCRuleRepository qcRuleRepository)
        {
            this.applicationRepository = applicationRepository;
            this.qcRuleRepository = qcRuleRepository;
        }

        public IResponse Handle(EnableRuleForApplication domainCommand)
        {
            ApplicationQC application = applicationRepository.GetApplication(domainCommand.ApplicationId);
            
            if (application==null || application.IsNotInstalled)
            {
                throw new ApplicationNotExistsException();
            }

            var qcRule = qcRuleRepository.GetRuleById(domainCommand.RuleId);

            application.EnableRule(qcRule);

            applicationRepository.Save(application.Changes);

            return new CommandSuccesfullyHandled();
         }
    }
}