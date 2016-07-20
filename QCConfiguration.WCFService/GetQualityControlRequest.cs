using System.Runtime.Serialization;

namespace QCConfiguration.WCFService
{
    [DataContract]
    public class GetQualityControlRequest
    {
        [DataMember]
        public int TestCode { get; set; }
    }
}