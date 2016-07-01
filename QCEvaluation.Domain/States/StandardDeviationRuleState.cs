using System.Collections.Generic;
using QCEvaluation.Domain.Events;

namespace QCEvaluation.Domain.States
{
    public class StandardDeviationRuleState : QCRuleState
    {
        public StandardDeviationRuleState(IEnumerable<QCRuleEvent> qcRuleEvents) : base(qcRuleEvents)
        {
        }

        internal int? StandardDeviationLimit { get; set; }
        internal int? NumberOfControls { get; set; }

        public void When(QCStandardDeviationRuleCreated qcStandardDeviationRuleCreated)
        {
            this.Id = qcStandardDeviationRuleCreated.QCRuleId;
            this.Name = qcStandardDeviationRuleCreated.Name;
            this.WithinControl = qcStandardDeviationRuleCreated.IsWithinControl;
            this.Comment = qcStandardDeviationRuleCreated.Comment;
            this.IsCreated = true;
            this.Active = true;

            this.NumberOfControls = qcStandardDeviationRuleCreated.NumberOfControls;
            this.StandardDeviationLimit = qcStandardDeviationRuleCreated.StandardDeviationLimit;
        }

        public void When(QCRuleNumberOfControlsUpdated qcRuleNumberOfControlsUpdated)
        {
            this.NumberOfControls = qcRuleNumberOfControlsUpdated.NumberOfControls;
        }

        public void When(QCRuleLimitsUpdated update)
        {
            this.StandardDeviationLimit = update.StandardDeviationLimit;
        }
    }
}