using System;
using Application.Payloads;
using ApplicationServices;
using QCConfiguration.Application.Responses;
using QCConfiguration.Domain.Repositories;

namespace QCConfiguration.Application.Commands.Handlers
{
    public class GetQualityControlHandler:IHandler<GetQualityControl,GetQualityControlResponse>
    {
        private readonly IQualityControlRepository repository;

        public GetQualityControlHandler(IQualityControlRepository repository)
        {
            this.repository = repository;
        }

        public GetQualityControlResponse Handle(GetQualityControl command)
        {
            GetQualityControlResponse returnValue;

            try
            {
                var qualityControl = repository.GetQualityControlForTestCode(command.TestCode);
                if (qualityControl!=null)
                {
                    returnValue = new GetQualityControlFound(new QualityControlPayload(qualityControl));
                }
                else
                {
                    returnValue = new QualityControlNotFound(command.TestCode);
                }
            }
            catch (Exception)
            {
                returnValue = new QualityControlRepositoryFailed(command.TestCode);
            }
            
            return returnValue;
        }
    }
}