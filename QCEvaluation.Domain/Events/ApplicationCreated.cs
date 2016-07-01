using System;

namespace QCEvaluation.Domain.Events
{
    public class ApplicationCreated : ApplicationEvent
    {
        private readonly int testCode;

        public ApplicationCreated(Guid applicationId, int testCode)
        {
            this.applicationId = applicationId;
            this.testCode = testCode;
        }

        public int TestCode
        {
            get { return testCode; }
        }
    }
}