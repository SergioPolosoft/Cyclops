using System.Runtime.Serialization;
using Application.Payloads;

namespace QCConfiguration.WCFService
{
    [DataContract]
    public class QualityControlDTO
    {
        public QualityControlDTO(QualityControlPayload qualityControlPayload)
        {
            this.TestCode = qualityControlPayload.TestCode;
            this.StandardDeviation = qualityControlPayload.StandardDeviation;
            this.TargetValue = qualityControlPayload.TargetValue;
        }

        [DataMember]
        public double TargetValue { get; set; }

        [DataMember]
        public double StandardDeviation { get; set; }

        [DataMember]
        public int TestCode { get; set; }
    }
}