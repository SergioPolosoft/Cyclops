using System.Collections.Generic;
using Application.Payloads;
using ApplicationServices;

namespace QCRoutine.Application.Responses
{
    public class GetResultsResponse:IResponse
    {
        public GetResultsResponse(List<QCResultPayload> listOfQcResults)
        {
            this.Results = listOfQcResults;
        }

        public CommandResult Status { get; protected set; }
        public IList<QCResultPayload> Results { get; private set; }
        public string Message { get; protected set; }
    }
}