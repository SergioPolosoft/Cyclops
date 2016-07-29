using System.Runtime.Serialization;
using LabConfiguration.Adapters.Service_References.QCEvaluationServiceReference;

namespace LabConfiguration.WCFService
{
    [DataContract]
    public class GetApplicationByTestCodeResponse
    {
        [DataMember]
        public RequestResult Status { get; set; }

        [DataMember]
        public ApplicationData Application { get; set; }
    }
}