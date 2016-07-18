using System;

namespace QCEvaluation.Domain.Events
{
    public class QCStandardDeviationRuleCreated: QCRuleEvent
    {
        public QCStandardDeviationRuleCreated(Guid ruleId, bool isWithinControl, string comment, string ruleName, int? numberOfControls, int? standardDeviationLimit) : base(ruleId)
        {
            IsWithinControl = isWithinControl;
            Comment = comment;
            Name = ruleName;
            this.NumberOfControls = numberOfControls;
            this.StandardDeviationLimit = standardDeviationLimit;
        }

        public int? StandardDeviationLimit { get; private set; }

        public bool IsWithinControl { get; private set; }
        public string Comment { get; private set; }
        public string Name { get; private set; }
        public int? NumberOfControls { get; private set; }
    }
}