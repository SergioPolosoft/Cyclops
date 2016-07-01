using System.Xml;
using ApplicationServices;

namespace InstrumentCommunication.TsnAdapter.Events
{
    public class UrgentInformationReceived:IEvent
    {
        public UrgentInformationReceived(XmlDocument xmlDocument)
        {
            this.XmlDocument = xmlDocument;
        }

        public XmlDocument XmlDocument { get; private set; }
    }
}