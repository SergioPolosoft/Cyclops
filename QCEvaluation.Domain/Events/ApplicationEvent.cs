using System;
using ApplicationServices;

namespace QCEvaluation.Domain.Events
{
    public class ApplicationEvent : AggreggateEvent
    {
        protected Guid applicationId;

        public Guid ApplicationId
        {
            get { return applicationId; }
            set { applicationId = value; }
        }
    }
}