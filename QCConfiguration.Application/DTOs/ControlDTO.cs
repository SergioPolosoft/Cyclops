namespace QCConfiguration.Application.DTOs
{
    public class ControlDTO
    {
        public ControlDTO(int testCode, double standardDeviation, double targetValue)
        {
            TestCode = testCode;
            StandardDeviation = standardDeviation;
            TargetValue = targetValue;
        }

        internal int TestCode { get; set; }

        public double StandardDeviation { get; set; }
        public double TargetValue { get; set; }
    }
}