using System;
using System.Collections.Generic;
using ApplicationServices;

namespace QCEvaluation.Domain.Repositories
{
    public interface IQCRuleRepository
    {
        QCRule GetRuleByName(string ruleName);
        void Save(IList<AggreggateEvent> events);
        bool ExistARuleWithName(string ruleName);
        StandardDeviationRule GetStandardDeviationRuleByName(string ruleName);
        QCRule GetRuleById(Guid ruleId);
        bool ExistsRule(Guid ruleId);
    }
}