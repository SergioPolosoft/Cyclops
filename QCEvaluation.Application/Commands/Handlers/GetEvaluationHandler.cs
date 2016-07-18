using System;
using ApplicationServices;
using QCEvaluation.Application.DTOs;
using QCEvaluation.Application.Responses;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Application.Commands.Handlers
{
    public class GetEvaluationHandler:IHandler<GetEvaluation, GetEvaluationResponse>
    {
        private readonly IEvaluationsRepository evaluationsRepository;

        public GetEvaluationHandler(IEvaluationsRepository evaluationsRepository)
        {
            this.evaluationsRepository = evaluationsRepository;
        }

        public GetEvaluationResponse Handle(GetEvaluation domainCommand)
        {
            var evaluation = evaluationsRepository.GetEvaluationFor(domainCommand.QCResultId);
            return new GetEvaluationResponse((EvaluationDTO) evaluation);
        }
    }
}