using System;
using ApplicationServices;
using Infrastructure.Repositories;
using QCRoutine.Adapters;
using QCRoutine.Application;
using QCRoutine.Application.Commands;
using WCFServices.Common;

namespace QCRoutine.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "IQCRoutineService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select IQCRoutineService.svc or IQCRoutineService.svc.cs at the Solution Explorer and start debugging.
    public class QcRoutineService : IQCRoutineService
    {
        private readonly QCRoutineServices qcRoutineService;

        public QcRoutineService()
        {
            qcRoutineService = new QCRoutineServices(new QCEvaluationAdapter(), new MongoDBQCResultsRepository(), new LabConfigurationAdapter());
        }

        public StoreQCResultResponse StoreQCResult(QCResult request)
        {
            var response = qcRoutineService.Handle(new StoreQCResult(request.TestCode, request.Result, request.MeasuredDate));
            if (response.Status == CommandResult.Success)
            {
                return new StoreQCResultResponse(){Status = RequestResult.Succesfull};
            }
            return new StoreQCResultResponse(){Status = RequestResult.Error};
        }
    }
}
