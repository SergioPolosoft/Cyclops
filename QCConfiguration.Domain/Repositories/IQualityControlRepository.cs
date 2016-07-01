

using System.Collections.Generic;
using ApplicationServices;
using QCConfiguration.Domain.Events;

namespace QCConfiguration.Domain.Repositories
{
    public interface IQualityControlRepository
    {
        QualityControl GetQualityControlForTestCode(int testCode);
        void Add(IList<AggreggateEvent> changes);
    }
}