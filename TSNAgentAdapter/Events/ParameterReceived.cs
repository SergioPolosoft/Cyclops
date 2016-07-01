using System.Xml;
using ApplicationServices;

namespace InstrumentCommunication.TsnAdapter.Events
{
    public class ParameterReceived : IEvent
    {
        public XmlDocument Document { get; private set; }

        public ParameterReceived(XmlDocument xmlDocument)
        {
            this.Document = xmlDocument;
        }
    }
}