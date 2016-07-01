using ApplicationServices;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Application.Commands.Handlers
{
    public class CreateStandardDeviationRuleHandler:IHandler<CreateStandardDeviationRule,IResponse>
    {
        private readonly IQCRuleRepository repository;

        public CreateStandardDeviationRuleHandler(IQCRuleRepository repository)
        {
            this.repository = repository;
        }

        public IResponse Handle(CreateStandardDeviationRule domainCommand)
        {
            var rule = new StandardDeviationRule();
            rule.CreateStandardDeviationRule(domainCommand.RuleName, domainCommand.WithinControl, domainCommand.Comment, domainCommand.NumberOfControls, domainCommand.StandardDeviationLimits, repository);
            
            repository.Save(rule.Changes);

            return new CommandSuccesfullyHandled();
        }
    }
}