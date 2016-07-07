using System;
using System.Collections.Generic;
using System.Linq;
using Application.Payloads;
using ApplicationServices;
using QCEvaluation.Domain.Events;
using QCEvaluation.Domain.Repositories;
using QCEvaluation.Domain.States;

namespace QCEvaluation.Domain
{
    public abstract class QCRule : Aggreggate<QCRuleState>
    {
        protected QCRule(IEnumerable<QCRuleEvent> changesForTheState) : base(new List<AggreggateEvent>())
        {
            state = GetQCRuleState(changesForTheState);            
        }

        protected abstract QCRuleState GetQCRuleState(IEnumerable<QCRuleEvent> changesForTheId);

        protected QCRule():this(new List<QCRuleEvent>())
        {}

        public bool Active
        {
            get { return state.Active; }
        }

        public string Name
        {
            get { return state.Name; }
        }

        public bool WithinControl
        {
            get { return state.WithinControl; }
        }

        public string Comment
        {
            get { return state.Comment; }
        }

        public Guid Id
        {
            get { return state.Id; }
        }

        public void UpdateComment(string newComment)
        {
            Apply(new QCRuleCommentUpdated(newComment, state.Id));
        }

        public void Deactivate()
        {
            if (IsCreated)
            {
                if (Active)
                {
                    Apply(new QCRuleDeactivated(state.Id));
                }
    
            }
            else
            {
                throw new InvalidOperationException("Trying to deactivate a not existing rule.");
            }
        }

        public bool IsCreated
        {
            get { return state.IsCreated; }
        }

        public static QCRule CreateRule(IEnumerable<QCRuleEvent> changesForTheId)
        {
            if (changesForTheId.Any(x=>x.GetType()==typeof(QCStandardDeviationRuleCreated)))
            {
                return new StandardDeviationRule(changesForTheId);
            }
            return null;
        }

        public void Reactivate()
        {
            if (IsCreated)
            {
                if (Active==false)
                {
                    Apply(new QCRuleReactivate(state.Id));
                }

            }
            else
            {
                throw new InvalidOperationException("Trying to deactivate a not existing rule.");
            }
        }

        public abstract EvaluationResult Evaluate(QCResult result, IQCResultsRepository resultsRepository, QualityControl qualityControlRepository);
    }
}