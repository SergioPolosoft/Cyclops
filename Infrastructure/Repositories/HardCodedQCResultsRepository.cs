using System.Collections.Generic;
using System.Linq;
using QCRoutine.Domain;

namespace Infrastructure.Repositories
{
    public class HardCodedQCResultsRepository : IQCResultsRepository
    {
        private readonly IList<QCResult> qcResults;

        public HardCodedQCResultsRepository()
        {
            qcResults = new List<QCResult>();
        }

        public IEnumerable<QCResult> GetResultsOrderedByDate(int numberOfResults)
        {
            return qcResults.OrderByDescending(x => x.MeasuredTime).Take(numberOfResults);
        }

        public void Add(QCResult qcResult)
        {
            this.qcResults.Add(qcResult);
        }
    }
}