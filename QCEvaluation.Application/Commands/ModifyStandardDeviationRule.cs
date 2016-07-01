using ApplicationServices;

namespace QCEvaluation.Application.Commands
{
    public class ModifyStandardDeviationRule : ICommand
    {
        public ModifyStandardDeviationRule(string ruleName, string comment, int numberOfControls, int standardDeviationLimits)
        {
            this.RuleName = ruleName;
            this.NewComment = comment;
            this.NewNumberOfControls = numberOfControls;
            this.NewStandardDeviationLimits = standardDeviationLimits;
        }

        public int NewStandardDeviationLimits { get; private set; }

        public int NewNumberOfControls { get; private set; }

        public string NewComment { get; private set; }

        public string RuleName { get; private set; }
    }
}