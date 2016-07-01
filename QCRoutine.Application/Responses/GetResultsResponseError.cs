using System;
using ApplicationServices;

namespace QCRoutine.Application.Responses
{
    public class GetResultsResponseError : GetResultsResponse
    {
        private Exception exception;

        public GetResultsResponseError(string message) : base(null)
        {
            this.Status = CommandResult.Error;
            this.Message = message;
        }

        public GetResultsResponseError(string message, Exception exception):this(message)
        {
            this.exception = exception;
        }
    }
}