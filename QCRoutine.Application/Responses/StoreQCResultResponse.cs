using ApplicationServices;

namespace QCRoutine.Application.Responses
{
    public class StoreQCResultResponse:IResponse
    {
        public CommandResult Status { get; protected set; }
        public string Message { get; internal set; }
    }
}