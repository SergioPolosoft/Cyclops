using Application.Payloads;

namespace LabConfiguration.Application.Responses
{
    public class FTPConfigurationPayload:IPayloadObject
    {
        public string DataBasket { get; private set; }
        public string Password { get; private set; }
        public string User { get; private set; }
    }
}