using System.Runtime.Serialization;

namespace QCEvaluation.WCFService
{
    [DataContract]
    public class CreateStandardDeviationRuleRequest
    {
        [DataMember]
        public string RuleName { get; set; }

        [DataMember]        
        public bool WithinControlValue { get; set; }

        [DataMember]        
        public string Comments { get; set; }
        
        [DataMember]
        public int NumberOfControls { get; set; }

        [DataMember]
        public int StandardDeviationLimits { get; set; }
    }
}