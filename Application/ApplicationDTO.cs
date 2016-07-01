namespace LabConfiguration.Application
{
    public class ApplicationDTO
    {
        private readonly int testCode;

        public ApplicationDTO(int testCode)
        {
            this.testCode = testCode;
        }

        public int TestCode
        {
            get { return testCode; }
        }

        public string SD1Value { get; set; }
        public string SD2Value { get; set; }
        public string SD3Value { get; set; }
    }
}