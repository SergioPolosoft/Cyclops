using System.Collections.Generic;
using System.Xml;

namespace Infrastructure.DTOs
{
    public class UrgentInformationDownloadDTO:XmlMessageDTO
    {
        public UrgentInformationDownloadDTO(XmlDocument xmlDocument):base(xmlDocument)
        {
        }

        public override string XmlName
        {
            get { return "UrgentInformationDownload.xml"; }
        }
    }
}