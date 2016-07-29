using System;
using System.Runtime.Serialization;

namespace QCEvaluation.WCFService
{
    [DataContract]
    public class QCResultDTO
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public double Value { get; set; }

        [DataMember]
        public int TestCode { get; set; }
    }
}