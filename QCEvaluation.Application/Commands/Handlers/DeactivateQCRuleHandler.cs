using System;
using ApplicationServices;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Application.Commands.Handlers
{
    public class DeactivateQCRuleHandler:IHandler<DeactivateQCRule,IResponse>
    {
        private readonly IQCRuleRepository repository;

        public DeactivateQCRuleHandler(IQCRuleRepository qcRulesRepository)
        {
            this.repository = qcRulesRepository;
        }

        public IResponse Handle(DeactivateQCRule domainCommand)
        {
            var rule = repository.GetRuleById(domainCommand.RuleId);
            if (rule==null)
            {
                throw new InvalidOperationException(string.Format("Ther rule with id {0} was not loaded.", domainCommand.RuleId));
            }
            rule.Deactivate();

            repository.Save(rule.Changes);

            return new CommandSuccesfullyHandled();
        }
    }
}