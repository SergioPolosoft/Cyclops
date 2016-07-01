using System.Collections.Generic;
using System.Linq;
using QCConfiguration.Domain;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class HardCodedQCResultsEvaluationRepository : IQCResultsRepository
    {
        private readonly IList<QCResult> qcresults = new List<QCResult>();

        public IList<QCResult> GetLastQCResults(int testCode, int numberOfResults)
        {
            return qcresults.Where(x => x.TestCode == testCode).Take(numberOfResults).ToList();
        }

        public void Add(QCResult qcResult)
        {
            this.qcresults.Add(qcResult);
        }
    }
}