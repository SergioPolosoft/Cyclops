using ApplicationServices;

namespace QCEvaluation.Application.Commands
{
    public class GetStandardDeviationRuleByName : ICommand
    {
        public GetStandardDeviationRuleByName(string ruleName)
        {
            this.RuleName = ruleName;
        }

        public string RuleName { get; set; }
    }
}