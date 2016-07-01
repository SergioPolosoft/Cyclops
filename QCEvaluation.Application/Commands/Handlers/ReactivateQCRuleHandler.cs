using ApplicationServices;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Application.Commands.Handlers
{
    public class ReactivateQCRuleHandler:IHandler<ReactivateQCRule,IResponse>
    {
        private readonly IQCRuleRepository repository;

        public ReactivateQCRuleHandler(IQCRuleRepository qcRuleRepository)
        {
            this.repository = qcRuleRepository;
        }

        public IResponse Handle(ReactivateQCRule domainCommand)
        {
            var rule = repository.GetRuleById(domainCommand.Id);

            rule.Reactivate();

            repository.Save(rule.Changes);

            return new CommandSuccesfullyHandled();
        }
    }
}