using System;
using System.Collections.Generic;
using ApplicationServices;

namespace QCEvaluation.Domain.Repositories
{
    public interface IQCApplicationRepository
    {
        void Save(IList<AggreggateEvent> domainEvents);
        ApplicationQC GetApplication(Guid applicationId);
        ApplicationQC GetApplicationByTestCode(int testCode);        
    }
}