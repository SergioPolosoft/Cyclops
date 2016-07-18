using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationServices;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Events;
using QCEvaluation.Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class InMemoryIQCApplicationRepository : IQCApplicationRepository
    {
        private readonly IList<ApplicationEvent> applicationEvents = new List<ApplicationEvent>();

        public void Save(IList<AggreggateEvent> domainEvents)
        {
            foreach (var aggreggateEvent in domainEvents)
            {
                this.applicationEvents.Add((ApplicationEvent) aggreggateEvent);
            }
        }

        public IEnumerable<AggreggateEvent> GetEventsById(Guid applicationId)
        {
            return applicationEvents.Where(x => x.ApplicationId == applicationId);
        }

        public ApplicationQC GetApplication(Guid applicationId)
        {
            return new ApplicationQC(GetEventsById(applicationId));
        }

        public ApplicationQC GetApplication(int testCode)
        {
            throw new NotImplementedException();
        }

        public ApplicationQC GetApplicationByTestCode(int testCode)
        {
            var creationEvent = applicationEvents.OfType<ApplicationCreated>().FirstOrDefault(x => x.TestCode == testCode);
            return creationEvent != null ? new ApplicationQC(GetEventsById(creationEvent.ApplicationId)) : null;
        }

        public bool ApplicationNotExists(int testCode)
        {
            throw new NotImplementedException();
        }
    }
}