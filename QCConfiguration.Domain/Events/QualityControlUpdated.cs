using System;

namespace QCConfiguration.Domain.Events
{
    public class QualityControlUpdated:QualityControlEvent
    {
        public QualityControlUpdated(Guid controlId, double targetValue, double standardDeviation) : base(controlId)
        {
            this.TargetValue = targetValue;
            this.StandardDeviation = standardDeviation;
        }

        public double StandardDeviation { get; private set; }
        public double TargetValue { get; private set; }
    }
}