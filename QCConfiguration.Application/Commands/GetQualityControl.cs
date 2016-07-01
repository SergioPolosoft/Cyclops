using ApplicationServices;

namespace QCConfiguration.Application.Commands
{
    public class GetQualityControl : ICommand
    {
        public GetQualityControl(int testCode)
        {
            this.TestCode = testCode;
        }

        public int TestCode { get; private set; }
    }
}