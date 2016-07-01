using System;
using ApplicationServices;

namespace QCEvaluation.Domain.Events
{
    public class QCRuleEvent:AggreggateEvent
    {
        public Guid QCRuleId { get; private set; }
        public string Comment { get; private set; }
        public string Name { get; private set; }
        public bool IsWithinControl { get; private set; }

        protected QCRuleEvent(Guid ruleId)
        {
            this.QCRuleId = ruleId;
        }

        protected QCRuleEvent(Guid ruleId, string ruleName, bool isWithinControl, string comment)
        {
            this.QCRuleId = ruleId;
            this.Name = ruleName;
            this.IsWithinControl = isWithinControl;
            this.Comment = comment;
        }

        public Guid Id
        {
            get { return QCRuleId; }
        }
    }
}