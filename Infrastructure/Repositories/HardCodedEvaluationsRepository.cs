using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using QCConfiguration.Domain;
using QCConfiguration.Domain.Repositories;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class HardCodedEvaluationsRepository : IEvaluationsRepository
    {
        private readonly IList<Evaluation> evaluations;

        public HardCodedEvaluationsRepository()
        {
            evaluations = new List<Evaluation>();
        }

        public EvaluationResult GetEvaluationFor(Guid qcResultId)
        {
            return this.evaluations.First(x => x.QCResultId == qcResultId).EvaluationResult;
        }

        public void Add(Evaluation evaluation)
        {
            this.evaluations.Add(evaluation);
        }
    }
}