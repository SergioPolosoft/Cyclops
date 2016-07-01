using System;
using System.Collections.Generic;
using ApplicationServices;
using QCEvaluation.Domain.Events;

namespace QCEvaluation.Domain.States
{
    public class ApplicationQCState : State
    {
        private readonly IList<Guid> rulesIdEnabled = new List<Guid>();

        public ApplicationQCState(IEnumerable<AggreggateEvent> changesForTheId)
            : base(changesForTheId)
        {
        }

        public ApplicationQCState() : base(new List<AggreggateEvent>())
        {
            
        }

        public void When(ApplicationCreated domainEvent)
        {
            this.Id = domainEvent.ApplicationId;
            this.TestCode = domainEvent.TestCode;
            this.Status = ApplicationStatus.Installed;
        }

        public ApplicationStatus Status { get; private set; }
        
        public void When(QCRuleEnabled ruleEnabled)
        {
            this.rulesIdEnabled.Add(ruleEnabled.RuleId);
        }

        public void When(QCRuleDisabled ruleDisabled)
        {
            this.rulesIdEnabled.Remove(ruleDisabled.RuleId);
        }

        public void When(ApplicationDeleted applicationDeleted)
        {
            this.Status = ApplicationStatus.Deleted;
        }

        public Guid Id { get; private set; }
        
        public IList<Guid> RulesIdEnabled
        {
            get { return rulesIdEnabled; }
        }

        public int TestCode { get; private set; }
    }
}