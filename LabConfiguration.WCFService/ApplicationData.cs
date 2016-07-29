using System.Runtime.Serialization;

namespace LabConfiguration.WCFService
{
    [DataContract]
    public class ApplicationData
    {
        [DataMember]
        public int TestCode { get; set; }
    }
}