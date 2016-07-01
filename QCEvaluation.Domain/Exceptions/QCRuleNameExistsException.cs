using System;

namespace QCEvaluation.Domain.Exceptions
{
    public class QCRuleNameExistsException:Exception
    {
        private string ruleName;

        public QCRuleNameExistsException(string ruleName)
        {
            this.ruleName = ruleName;
        }
    }
}