using ApplicationServices;

namespace LabConfiguration.Application.Responses
{
    public class GetFTPConfigurationResponse : IResponse
    {
        public global::Application.Payloads.FTPConfigurationPayload FTPConfiguration;
        public CommandResult Status { get; private set; }
        public string Message { get; private set; }
    }
}