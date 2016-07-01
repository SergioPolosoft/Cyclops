using System.Xml;
using System.Xml.Linq;
using ApplicationServices;

namespace InstrumentCommunication.Application.Commands
{
    public class ParameterConfirmationCommand : ICommand
    {
        private readonly XDocument document;

        public ParameterConfirmationCommand(XmlDocument xmlDocument)
        {
            document = XDocument.Parse(xmlDocument.InnerXml);
        }

        public XDocument XmlDocument
        {
            get { return this.document; }
        }
    }
}