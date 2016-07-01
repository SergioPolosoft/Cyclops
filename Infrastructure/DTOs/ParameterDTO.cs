using System.Xml;

namespace Infrastructure.DTOs
{
    public class ParameterDTO : XmlMessageDTO
    {
        public ParameterDTO(XmlDocument xmlDocument) : base(xmlDocument)
        {
        }

        public override string XmlName
        {
            get { return "Application.xml"; }
        }
    }
}