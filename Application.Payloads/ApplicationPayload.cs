namespace Application.Payloads
{
    public class ApplicationPayload:IPayloadObject
    {
        private readonly LabConfiguration.Domain.ApplicationTest applicationTest;
        private readonly int testCode;

        public ApplicationPayload(LabConfiguration.Domain.ApplicationTest applicationTest)
        {
            this.applicationTest = applicationTest;
        }

        public ApplicationPayload(int testCode)
        {
            this.testCode = testCode;
        }

        public int TestCode
        {
            get {
                if (applicationTest != null)
                {
                    return applicationTest.ApplicationCode;
                }
                return testCode;
            }
        }
    }
}