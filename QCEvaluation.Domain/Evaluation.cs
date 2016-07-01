using System;

namespace QCEvaluation.Domain
{
    public class Evaluation
    {
        private readonly Guid qcResultId;
        private readonly EvaluationResult evaluation;

        public Evaluation(Guid qcResultId, EvaluationResult evaluationResult)
        {
            this.qcResultId = qcResultId;
            this.evaluation = evaluationResult;
        }

        public Guid QCResultId
        {
            get { return qcResultId; }
        }

        public EvaluationResult EvaluationResult
        {
            get { return evaluation; }
        }
    }
}