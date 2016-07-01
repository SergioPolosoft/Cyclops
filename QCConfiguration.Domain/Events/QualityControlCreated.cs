using System;

namespace QCConfiguration.Domain.Events
{
    public class QualityControlCreated : QualityControlEvent
    {
        public QualityControlCreated(Guid controlId, int testCode, double targetValue, double standardDeviation):base(controlId)
        {
            this.TestCode = testCode;
            this.TargetValue = targetValue;
            this.StandardDeviation = standardDeviation;
        }

        public double StandardDeviation { get; set; }

        public double TargetValue { get; set; }

        public int TestCode { get; set; }
    }
}