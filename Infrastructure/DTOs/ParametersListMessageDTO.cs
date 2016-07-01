using System.Xml;

namespace Infrastructure.DTOs
{
    public class ParametersListMessageDTO:XmlMessageDTO
    {
        public ParametersListMessageDTO(XmlDocument xmlMessage) : base(xmlMessage)
        {
        }


        public override string XmlName
        {
            get { return "ListOfApp.xml"; }
        }
    }
}