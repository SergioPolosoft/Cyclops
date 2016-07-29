using System.Runtime.Serialization;
using WCFServices.Common;

namespace QCRoutine.WCFService
{
    [DataContract]
    public class StoreQCResultResponse
    {
        [DataMember]
        public RequestResult Status { get; set; }
    }
}