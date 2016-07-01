using System;
using ApplicationServices;

namespace QCEvaluation.Application.Commands
{
    public class DeactivateQCRule : ICommand
    {
        public DeactivateQCRule(Guid ruleId)
        {
            this.RuleId = ruleId;
        }

        public Guid RuleId { get; private set; }
    }
}