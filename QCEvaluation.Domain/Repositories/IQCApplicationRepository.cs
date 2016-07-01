using System;
using System.Collections.Generic;
using ApplicationServices;
using QCEvaluation.Domain.Events;

namespace QCEvaluation.Domain.Repositories
{
    public interface IQCApplicationRepository
    {
        void Save(IList<AggreggateEvent> domainEvents);
        IEnumerable<AggreggateEvent> GetEventsById(Guid applicationId);
        ApplicationQC GetApplication(Guid applicationId);
        ApplicationQC GetApplication(int testCode);
        ApplicationQC GetApplicationByTestCode(int testCode);        
    }
}