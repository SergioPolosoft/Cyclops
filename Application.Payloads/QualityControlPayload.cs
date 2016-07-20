using System.Runtime.Serialization;
using QCConfiguration.Domain;

namespace Application.Payloads
{
    [DataContract]
    public class QualityControlPayload : IPayloadObject
    {
        public QualityControlPayload(QualityControl qualityControl)
        {
            this.StandardDeviation = qualityControl.StandardDeviation;
            this.TargetValue = qualityControl.TargetValue;
            this.TestCode = qualityControl.TestCode;
        }

        public QualityControlPayload()
        {
        }

        [DataMember]
        public double TargetValue { get; set; }

        [DataMember]
        public double StandardDeviation { get; set; }

        [DataMember]
        public int TestCode { get; set; }
    }
}