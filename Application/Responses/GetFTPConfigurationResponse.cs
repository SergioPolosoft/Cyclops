using ApplicationServices;

namespace LabConfiguration.Application.Responses
{
    public class GetFTPConfigurationResponse : IResponse
    {
        public global::Application.Payloads.FTPConfigurationPayload FTPConfiguration;
        public CommandResult Status { get; internal set; }
        public string Message { get; internal set; }
    }
}