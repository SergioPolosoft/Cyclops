using System.Runtime.Serialization;
using Application.Payloads;

namespace QCEvaluation.WCFService
{
    [DataContract]
    public class GetStandardDeviationRuleResponse
    {
        public GetStandardDeviationRuleResponse(StandardDeviationRulePayload standardDeviationRulePayload)
        {
            throw new System.NotImplementedException();
        }

        [DataMember]
        public StandardDeviationRulePayload StandardDeviationRule { get; set; }
    }
}