using Application.Payloads;
using ApplicationServices;

namespace LabConfiguration.Application.Responses
{
    public class GetApplicationResponse : IResponse
    {
        public CommandResult Status { get; internal set; }
        public string Message { get; internal set; }
        public ApplicationPayload Application { get; internal set; }
    }
}