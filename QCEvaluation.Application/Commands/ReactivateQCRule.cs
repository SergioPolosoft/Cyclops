using System;
using ApplicationServices;

namespace QCEvaluation.Application.Commands
{
    public class ReactivateQCRule : ICommand
    {
        public ReactivateQCRule(Guid ruleId)
        {
            this.Id = ruleId;
        }

        public Guid Id { get; set; }
    }
}