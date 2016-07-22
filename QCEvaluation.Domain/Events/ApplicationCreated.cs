using System;

namespace QCEvaluation.Domain.Events
{
    public class ApplicationCreated : ApplicationEvent
    {
        public ApplicationCreated(Guid applicationId, int testCode)
        {
            this.applicationId = applicationId;
            this.TestCode = testCode;
        }

        public int TestCode { get; private set; }
    }
}