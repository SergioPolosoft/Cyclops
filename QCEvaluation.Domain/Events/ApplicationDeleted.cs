using System;
using ApplicationServices;

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