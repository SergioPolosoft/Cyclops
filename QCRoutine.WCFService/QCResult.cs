using System;
using System.Runtime.Serialization;

namespace QCRoutine.WCFService
{
    [DataContract]
    public class QCResult
    {
        [DataMember]
        public int TestCode { get; set; }

        [DataMember]
        public double Result { get; set; }

        [DataMember]
        public DateTime MeasuredDate { get; set; }
    }
}