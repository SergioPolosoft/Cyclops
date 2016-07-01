using System.Collections.Generic;
using Application.Payloads;
using ApplicationServices;

namespace QCRoutine.Application.Responses
{
    public class GetResultsResponse:IResponse
    {
        public CommandResult Status { get; private set; }
        public IList<QCResultPayload> Results { get; private set; }
    }
}