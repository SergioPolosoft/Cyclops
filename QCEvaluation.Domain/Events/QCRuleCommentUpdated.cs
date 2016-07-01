using System;

namespace QCEvaluation.Domain.Events
{
    public class QCRuleCommentUpdated : QCRuleEvent
    {
        public QCRuleCommentUpdated(string newComment, Guid ruleId) : base(ruleId)
        {
            this.Comment = newComment;
        }

        public string Comment { get; private set; }
    }
}