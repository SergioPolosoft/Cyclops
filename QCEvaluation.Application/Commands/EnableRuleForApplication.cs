using System;
using ApplicationServices;

namespace QCEvaluation.Application.Commands
{
    public class EnableRuleForApplication : ICommand
    {
        private readonly Guid ruleId;
        private readonly Guid applicationId;
        private readonly int testCode;

        public EnableRuleForApplication(Guid ruleId, Guid applicationId, int testCode)
        {
            this.ruleId = ruleId;
            this.applicationId = applicationId;
            this.testCode = testCode;
        }

        public Guid ApplicationId
        {
            get { return applicationId; }
        }

        public Guid RuleId
        {
            get { return ruleId; }
        }

        public int TestCode
        {
            get { return testCode; }
        }
    }
}