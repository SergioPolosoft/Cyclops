using ApplicationServices;

namespace QCEvaluation.Application.Events
{
    public class ApplicationInstalled:IEvent
    {
        public ApplicationInstalled(int applicationCode)
        {
            this.TestCode = applicationCode;
        }

        public int TestCode { get; private set; }
    }
}