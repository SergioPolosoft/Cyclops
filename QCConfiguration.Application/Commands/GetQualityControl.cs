using ApplicationServices;

namespace QCConfiguration.Application.Commands
{
    public class GetQualityControl : ICommand
    {
        public GetQualityControl(int testCode)
        {
            throw new System.NotImplementedException();
        }

        public int TestCode { get; private set; }
    }
}