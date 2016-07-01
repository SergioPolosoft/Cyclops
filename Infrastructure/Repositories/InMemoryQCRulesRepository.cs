using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationServices;
using QCConfiguration.Domain;
using QCConfiguration.Domain.Events;
using QCConfiguration.Domain.Repositories;
using QCConfiguration.Domain.States;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Events;
using QCEvaluation.Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class InMemoryQCRulesRepository : IQCRuleRepository
    {
        private readonly List<QCRuleEvent> changes = new List<QCRuleEvent>();

        public QCRule GetRuleByName(string ruleName)
        {
            return this.GetStandardDeviationRuleByName(ruleName);
        }

        public void Save(IList<AggreggateEvent> events)
        {
            foreach (var domainEvent in events)
            {
                this.changes.Add((QCRuleEvent) domainEvent);
            }
        }

        public bool ExistARuleWithName(string ruleName)
        {
            return this.changes.OfType<QCStandardDeviationRuleCreated>().Any(x => x.Name == ruleName);
        }

        public StandardDeviationRule GetStandardDeviationRuleByName(string ruleName)
        {
            var changesForTheRule = changes.First(x => x.Name == ruleName);
            var changesForTheId = changes.Where(x => x.QCRuleId == changesForTheRule.QCRuleId);

            var qcRule = new StandardDeviationRule(changesForTheId);

            return qcRule;
        }

        public QCRule GetRuleById(Guid ruleId)
        {
            var changesForTheId = changes.OfType<QCRuleEvent>().Where(x => x.QCRuleId == ruleId);

            return QCRule.CreateRule(changesForTheId);
        }

        public bool ExistsRule(Guid ruleId)
        {
            return this.changes.OfType<QCStandardDeviationRuleCreated>().Any(x => x.Id == ruleId);
        }
    }
}