using System;
using System.Linq;
using Application.Payloads;
using ApplicationServices;
using QCRoutine.Application.Responses;
using QCRoutine.Domain;

namespace QCRoutine.Application.Commands.Handlers
{
    public class GetQCResultHandler : IHandler<GetResultsByDate,GetResultsResponse>
    {
        private readonly IQCResultsRepository qualityControlsResultsRepository;

        public GetQCResultHandler(IQCResultsRepository qualityControlsResultsRepository)
        {
            this.qualityControlsResultsRepository = qualityControlsResultsRepository;
        }

        public GetResultsResponse Handle(GetResultsByDate command)
        {
            GetResultsResponse result;
            if (command.NumberOfResults<=0)
            {
                result = new GetResultsResponseError(QCRoutineMessages.InvalidNumberOfResults);
            }
            else
            {
                result = GetQCResults(command);
            }

            return result;
        }

        private GetResultsResponse GetQCResults(GetResultsByDate command)
        {
            GetResultsResponse result;
            try
            {
                var qcResults = qualityControlsResultsRepository.GetResultsOrderedByDate(command.NumberOfResults);
                var listOfQCResults = qcResults.Select(qcResult => new QCResultPayload(qcResult)).ToList();
                result = new GetResultsResponse(listOfQCResults);
            }
            catch (Exception exception)
            {
                result = new GetResultsResponseError(QCRoutineMessages.ErrorOcurredAccessingRepository, exception);
            }
            return result;
        }
    }
}