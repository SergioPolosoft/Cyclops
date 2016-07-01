using Application.Payloads;
using ApplicationServices;

namespace InstrumentCommunication.Application.Responses
{
    public class FTPConfigurationPayload:IPayloadObject
    {
        public string DataBasket { get; private set; }
        public string Password { get; private set; }
        public string User { get; private set; }
    }
}