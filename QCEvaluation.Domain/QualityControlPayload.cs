using QCConfiguration.Domain;

namespace QCEvaluation.Domain
{
    public class QualityControlPayload
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
    }
}