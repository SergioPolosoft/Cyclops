using System.Runtime.Serialization;
using WCFServices.Common;

namespace QCEvaluation.WCFService
{
    [DataContract]
    public class CreateStandardDeviationRuleResponse
    {
        public CreateStandardDeviationRuleResponse(RequestResult requestResult, string message)
        {
            this.RequestResult = requestResult;
            this.Message = message;
        }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public RequestResult RequestResult { get; set; }
    }
}