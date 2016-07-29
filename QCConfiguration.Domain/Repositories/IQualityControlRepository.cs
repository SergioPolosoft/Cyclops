

using System.Collections.Generic;
using ApplicationServices;

namespace QCConfiguration.Domain.Repositories
{
    public interface IQualityControlRepository
    {
        QualityControl GetQualityControlForTestCode(int testCode);
        void Add(IList<AggreggateEvent> changes);
    }
}