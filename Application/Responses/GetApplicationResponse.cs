using ApplicationServices;

namespace LabConfiguration.Application.Responses
{
    public class GetApplicationResponse : IResponse
    {
        public CommandResult Status { get; private set; }
    }
}