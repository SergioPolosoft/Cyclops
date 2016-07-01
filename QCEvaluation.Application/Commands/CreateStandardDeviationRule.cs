
using ApplicationServices;

namespace QCEvaluation.Application.Commands
{
    public class CreateStandardDeviationRule : ICommand
    {
        public CreateStandardDeviationRule(string ruleName, bool withingControlValue, string comment, int numberOfControls, int standardDeviationLimits)
        {
            this.RuleName = ruleName;
            this.WithinControl = withingControlValue;
            this.Comment = comment;
            this.NumberOfControls = numberOfControls;
            this.StandardDeviationLimits = standardDeviationLimits;
        }

        public int StandardDeviationLimits { get; private set; }

        public int NumberOfControls { get; private set; }

        public string Comment { get; private set; }

        public bool WithinControl { get; private set; }

        public string RuleName { get; private set; }
    }
}