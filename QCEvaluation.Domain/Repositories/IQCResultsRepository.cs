using System.Collections.Generic;

namespace QCEvaluation.Domain.Repositories
{
    public interface IQCResultsRepository
    {
        IList<QCResult> GetLastQCResults(int testCode, int numberOfResults);

        void Add(QCResult qcResult);
    }
}