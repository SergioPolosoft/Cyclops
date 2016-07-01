using System.Xml;

namespace Infrastructure.DTOs
{
    public abstract class XmlMessageDTO : MessageDTO
    {
        protected XmlMessageDTO(XmlDocument xmlDocument)
        {
            this.XmlMessage = xmlDocument;
        }

        public XmlDocument XmlMessage { get; internal set; }

        public abstract string XmlName { get; }
       
    }
}