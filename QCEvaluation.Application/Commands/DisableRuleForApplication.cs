using System;
using ApplicationServices;
using QCEvaluation.Domain;

namespace QCEvaluation.Application.Commands
{
    public class DisableRuleForApplication : ICommand
    {
        private QCRule rule;
        private Guid applicationId;

        public DisableRuleForApplication(QCRule rule, Guid applicationId)
        {
            this.rule = rule;
            this.applicationId = applicationId;
        }

        public Guid ApplicationId { get { return applicationId; } }

        public QCRule Rule
        {
            get { return rule; }
        }
    }
}