using System.Collections.Generic;

namespace QCRoutine.Domain
{
    public interface IQCResultsRepository
    {
        IEnumerable<QCResult> GetResultsOrderedByDate(int numberOfResults);
        void Add(QCResult qcResult);
    }
}