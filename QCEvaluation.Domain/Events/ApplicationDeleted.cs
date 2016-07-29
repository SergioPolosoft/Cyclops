using System;

namespace QCEvaluation.Domain.Events
{
    public class ApplicationDeleted : ApplicationEvent
    {
        public ApplicationDeleted(Guid applicationId)
        {
            this.applicationId = applicationId;
        }
       
    }
}