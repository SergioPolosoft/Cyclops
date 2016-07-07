using QCConfiguration.Domain;

namespace Application.Payloads
{
    public class QualityControlPayload : IPayloadObject
    {
        private readonly QualityControl qualityControl;

        public QualityControlPayload(QualityControl qualityControl)
        {
            this.qualityControl = qualityControl;
        }

        public double TargetValue
        {
            get { return qualityControl.TargetValue; }
        }

        public double StandardDeviation { get { return qualityControl.StandardDeviation; } }

        public int TestCode
        {
            get { return qualityControl.TestCode; }
        }
    }
}