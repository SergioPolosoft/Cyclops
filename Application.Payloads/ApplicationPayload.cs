namespace Application.Payloads
{
    public class ApplicationPayload:IPayloadObject
    {
        private readonly LabConfiguration.Domain.ApplicationTest applicationTest;

        public ApplicationPayload(LabConfiguration.Domain.ApplicationTest applicationTest)
        {
            this.applicationTest = applicationTest;
        }

        public int TestCode { get { return applicationTest.ApplicationCode; } }
    }
}