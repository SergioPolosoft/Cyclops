using ApplicationServices;

namespace QCRoutine.Application.Responses
{
    public class QCResultNotStoredResponse : StoreQCResultResponse
    {
        public QCResultNotStoredResponse(string reson)
        {
            this.Status = CommandResult.Error;
            this.Reason = reson;
        }

        public string Reason { get; private set; }
    }
}