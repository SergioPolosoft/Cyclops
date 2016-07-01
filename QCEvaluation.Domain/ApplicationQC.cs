using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationServices;
using QCEvaluation.Domain.Events;
using QCEvaluation.Domain.Repositories;
using QCEvaluation.Domain.States;

namespace QCEvaluation.Domain
{
    public class ApplicationQC:Aggreggate<ApplicationQCState>
    {
        public ApplicationQC(IEnumerable<AggreggateEvent> changes) : base(changes)
        {
        }

        public ApplicationQC(params AggreggateEvent[] changes):this(changes.ToList())
        {
            
        }

        private bool Installed
        {
            get { return state.Status == ApplicationStatus.Installed; }
        }

        public void Create()
        {
            Apply(new ApplicationCreated(Guid.NewGuid(),17302));
        }

        public void EnableRule(QCRule rule)
        {
            if (rule == null)
            {
                throw new ArgumentNullException();
            }

            if (this.IsNotInstalled)
            {
                this.Create();
            }

            if (!this.state.RulesIdEnabled.Contains(rule.Id))
            {
                Apply(new QCRuleEnabled(this.Id, rule.Id));    
            }
        }

        public IEnumerable<QCRule> GetRulesEnabled(IQCRuleRepository repository)
        {
            return state.RulesIdEnabled.Select(repository.GetRuleById);
        }

        public Guid Id
        {
            get { return state.Id; }
        }

        public bool IsNotInstalled
        {
            get { return Installed == false; }
        }
       
        public int TestCode
        {
            get { return state.TestCode; }
        }

        public ApplicationStatus Status
        {
            get { return state.Status; }
        }

        public bool HasRuleAssigned(QCRule rule)
        {
            return this.state.RulesIdEnabled.Contains(rule.Id);
        }

        public void DisableRule(QCRule rule)
        {
            if (this.HasRuleAssigned(rule))
            {
                Apply(new QCRuleDisabled(this.Id, rule.Id));
            }
        }

        public void Create(Guid applicationId, int testCode)
        {
            if (IsNotInstalled)
            {
                Apply(new ApplicationCreated(applicationId, testCode));
            }
        }

        public void Delete()
        {
            if (Installed)
            {
                Apply(new ApplicationDeleted(this.Id));    
            }
        }
    }
}
