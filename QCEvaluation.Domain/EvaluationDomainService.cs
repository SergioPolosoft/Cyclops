using System.Collections.Generic;
using System.Linq;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Domain
{
    public class EvaluationDomainService
    {
        private readonly IQCResultsRepository resultsRepository;
        
        public EvaluationDomainService(IQCResultsRepository resultsRepository)
        {
            this.resultsRepository = resultsRepository;
        }

        public EvaluationResult Evaluate(QCResult qcResult, IEnumerable<QCRule> qcRules, QualityControlPayload qualityControl)
        {
            EvaluationResult evaluationResult = EvaluationResult.Ok; 
            
            foreach (var ruleEvaluation in qcRules.Select(qcRule => qcRule.Evaluate(qcResult, resultsRepository, qualityControl)))
            {
                switch (ruleEvaluation)
                {
                    case EvaluationResult.Error:
                        return ruleEvaluation;
                    case EvaluationResult.NotEnoughData:
                        evaluationResult = EvaluationResult.NotEnoughData;
                        break;
                }
            }
            return evaluationResult;
        }
    }
}