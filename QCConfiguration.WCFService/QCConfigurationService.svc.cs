using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ApplicationServices;
using Infrastructure.Repositories;
using QCConfiguration.Application.Commands;
using QCConfiguration.Application.Responses;
using WCFServices.Common;

namespace QCConfiguration.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class QCConfigurationService : IQcConfigurationService
    {
        private readonly Application.IQCConfigurationServices applicationServices;

        public QCConfigurationService()
        {
            this.applicationServices = new Application.QCConfigurationServices(new MongoDBQCRepository());
        }

        public GetQualityControlResponse GetQualityControl(GetQualityControlRequest request)
        {
            var response = applicationServices.Handle(new GetQualityControl(request.TestCode));
            var getQualityControlFound = response as GetQualityControlFound;
            if (getQualityControlFound != null)
            {                
                return new GetQualityControlResponse(RequestResult.Succesfull, new QualityControlDTO(getQualityControlFound.QualityControl));
            }
            return new GetQualityControlResponse(RequestResult.Error,response.Message);
        }
    }
}
