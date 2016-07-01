using System;

namespace QCConfiguration.Application.DTOs
{
    public class ControlDTO
    {
        public ControlDTO(int testCode, double range1SD, double targetValue)
        {
            TestCode = testCode;
            Range1SD = range1SD;
            TargetValue = targetValue;
        }

        internal int TestCode { get; set; }

        public double Range1SD { get; set; }
        public double TargetValue { get; set; }
    }
}