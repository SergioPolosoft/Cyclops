using System;
using ApplicationServices;

namespace QCEvaluation.Domain.Events
{
    public class ApplicationDeleted : AggreggateEvent
    {
        private Guid applicationId;

        public ApplicationDeleted(Guid applicationId)
        {
            this.Id = Guid.NewGuid();
            this.applicationId = applicationId;
        }

        public Guid Id { get; private set; }
    }
}