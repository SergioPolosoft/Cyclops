using System;

namespace QCEvaluation.Domain.Repositories
{
    public interface IEvaluationsRepository
    {
        EvaluationResult GetEvaluationFor(Guid qcResultId);
        void Add(Evaluation evaluation);
    }
}