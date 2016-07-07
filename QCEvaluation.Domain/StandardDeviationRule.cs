using System;
using System.Collections.Generic;
using System.Linq;
using Application.Payloads;
using QCEvaluation.Domain.Events;
using QCEvaluation.Domain.Exceptions;
using QCEvaluation.Domain.Repositories;
using QCEvaluation.Domain.States;

namespace QCEvaluation.Domain
{
    public class StandardDeviationRule : QCRule
    {
        public StandardDeviationRule(IEnumerable<QCRuleEvent> changesForTheState) : base(changesForTheState)
        {
        }

        public StandardDeviationRule()
        {
        }

        public int? NumberOfControls
        {
            get { return ((StandardDeviationRuleState) state).NumberOfControls; }
        }

        public int? StandardDeviationLimit
        {
            get { return ((StandardDeviationRuleState)state).StandardDeviationLimit; }
        }

        public void CreateStandardDeviationRule(string ruleName, bool isWithinContol, string comment, int? numberOfControls, int? standardDeviationLimit, IQCRuleRepository rulesRepository)
        {
            if (rulesRepository.ExistARuleWithName(ruleName))
            {
                throw new QCRuleNameExistsException(ruleName);
            }

            Apply(new QCStandardDeviationRuleCreated(Guid.NewGuid(), isWithinContol,comment, ruleName, numberOfControls, standardDeviationLimit));
        }

        public void UpdateNumberOfControls(int numberOfControls)
        {
            Apply(new QCRuleNumberOfControlsUpdated(numberOfControls, state.Id));
        }

        public void UpdateLimits(int standardDeviationLimit)
        {
            Apply(new QCRuleLimitsUpdated(standardDeviationLimit, state.Id));
        }

        protected override QCRuleState GetQCRuleState(IEnumerable<QCRuleEvent> changesForTheId)
        {
            return new StandardDeviationRuleState(changesForTheId);
        }

        public override EvaluationResult Evaluate(QCResult result, IQCResultsRepository resultsRepository, QualityControl qualityControl)
        {
            var evaluationResult = EvaluationResult.NotEnoughData;
            
            var listOfResults = resultsRepository.GetLastQCResults(result.TestCode, this.NumberOfControls.Value);
            if (listOfResults!=null && listOfResults.Count == this.NumberOfControls.Value)
            {
                var upperRange = qualityControl.TargetValue +
                                     (qualityControl.StandardDeviation * this.StandardDeviationLimit);
                var belowRange = qualityControl.TargetValue -
                                     (qualityControl.StandardDeviation * this.StandardDeviationLimit);
                
                if (listOfResults.All(x=>x.Result > upperRange || x.Result < belowRange))
                {
                    evaluationResult = EvaluationResult.Error;
                }
                else
                {
                    evaluationResult = EvaluationResult.Ok;
                }

            }

            return evaluationResult;
        }
    }
}