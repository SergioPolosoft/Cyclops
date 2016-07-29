using System;

namespace QCEvaluation.Domain
{
    public class Evaluation
    {
        private Guid qcResultId;
        private readonly EvaluationResult evaluation;
        
        public Evaluation(Guid qcResultId, EvaluationResult evaluationResult)
        {
            this.Id = Guid.NewGuid();
            this.qcResultId = qcResultId;
            this.evaluation = evaluationResult;
        }

        public Guid Id { get; set; }

        public Guid QCResultId
        {
            get { return qcResultId; }
            private set { qcResultId = value; }
        }

        public EvaluationResult EvaluationResult
        {
            get { return evaluation; }
        }
    }
}