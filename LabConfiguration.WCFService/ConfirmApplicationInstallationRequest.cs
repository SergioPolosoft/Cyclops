using System.Runtime.Serialization;

namespace LabConfiguration.WCFService
{
    [DataContract]
    public class ConfirmApplicationInstallationRequest
    {
        [DataMember]
        public int ApplicationTestCode { get; private set; }
    }
}