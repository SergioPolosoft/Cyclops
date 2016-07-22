using System.Runtime.Serialization;
using Application.Payloads;
using WCFServices.Common;

namespace QCConfiguration.WCFService
{
    [DataContract]
    public class GetQualityControlResponse
    {
        public GetQualityControlResponse(RequestResult requestResult, QualityControlDTO qualityControl)
        {
            this.RequestResult = requestResult;
            this.QualityControl = qualityControl;
        }

        public GetQualityControlResponse(RequestResult requestResult, string message)
        {
            this.RequestResult = requestResult;
            this.Message = message;
        }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public QualityControlDTO QualityControl { get; set; }

        [DataMember]
        public RequestResult RequestResult { get; set; }
    }
}