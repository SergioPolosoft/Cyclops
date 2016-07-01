using ApplicationServices;

namespace LabConfiguration.Application.Commands
{
    public class GetApplicationCommand:ICommand
    {
        private int testCode;

        public GetApplicationCommand(int testCode)
        {
            this.testCode = testCode;
        }

        public int TestCode {
            get
            {
                return this.testCode;
                
            }
        }
    }
}