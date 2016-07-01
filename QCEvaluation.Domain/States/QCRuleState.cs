using System;
using System.Collections.Generic;
using ApplicationServices;
using QCEvaluation.Domain.Events;

namespace QCEvaluation.Domain.States
{
    public class QCRuleState: State
    {
        public bool Active { get; set; }
        public string Name { get; protected set; }
        public Guid Id { get; protected set; }
        public bool WithinControl { get; protected set; }
        public string Comment { get; protected set; }
        public bool IsCreated { get; internal set; }

        public QCRuleState(IEnumerable<QCRuleEvent> changesForTheId) : base(changesForTheId)
        {
        }

        public QCRuleState() : base(new List<AggreggateEvent>())
        {
            
        }

        public void When(QCRuleCommentUpdated commentUpdated)
        {
            this.Comment = commentUpdated.Comment;
        }

        public void When(QCRuleDeactivated update)
        {
            this.Active = false;
        }

        public void When(QCRuleReactivate update)
        {
            this.Active = true;
        }
    }
}