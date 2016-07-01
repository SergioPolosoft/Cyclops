using System;
using ApplicationServices;
using QCConfiguration.Application.Responses;

namespace QCConfiguration.Application.Commands.Handlers
{
    public class GetQualityControlHandler:IHandler<GetQualityControl,GetQualityControlResponse>
    {
        public GetQualityControlResponse Handle(GetQualityControl command)
        {
            throw new NotImplementedException();
        }
    }
}