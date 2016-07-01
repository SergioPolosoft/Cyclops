using System;
using ApplicationServices;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Application.Commands.Handlers
{
    public class ModifyStandardDeviationRuleHandler:IHandler<ModifyStandardDeviationRule, IResponse>
    {
        private readonly IQCRuleRepository repository;

        public ModifyStandardDeviationRuleHandler(IQCRuleRepository qcRulesRepository)
        {
            this.repository = qcRulesRepository;
        }

        public IResponse Handle(ModifyStandardDeviationRule domainCommand)
        {
            var qcRule = repository.GetStandardDeviationRuleByName(domainCommand.RuleName);
            if (qcRule==null)
            {
                throw new InvalidOperationException(string.Format("The rule {0} that wants to be modified does not exist.", domainCommand.RuleName));
            }
            qcRule.UpdateComment(domainCommand.NewComment);
            qcRule.UpdateLimits(domainCommand.NewStandardDeviationLimits);
            qcRule.UpdateNumberOfControls(domainCommand.NewNumberOfControls);

            repository.Save(qcRule.Changes);

            return new CommandSuccesfullyHandled();
        }
    }
}