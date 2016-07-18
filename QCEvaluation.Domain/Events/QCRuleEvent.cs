using System;
using ApplicationServices;

namespace QCEvaluation.Domain.Events
{
    public class QCRuleEvent:AggreggateEvent
    {
        public Guid QCRuleId { get; private set; }
       
        protected QCRuleEvent(Guid ruleId)
        {
            this.QCRuleId = ruleId;
        }

        public QCRuleEvent()
        {
            
        }
    }
}