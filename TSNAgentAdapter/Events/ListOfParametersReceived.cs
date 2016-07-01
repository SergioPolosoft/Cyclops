using System.Xml;
using ApplicationServices;

namespace InstrumentCommunication.TsnAdapter.Events
{
    public class ListOfParametersReceived : IEvent
    {
        public XmlDocument XmlMessage { get; private set; }

        public ListOfParametersReceived(XmlDocument xmlMessage)
        {
            this.XmlMessage = xmlMessage;
        }
       
    }
}