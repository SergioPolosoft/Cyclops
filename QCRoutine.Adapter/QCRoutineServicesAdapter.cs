using System;
using System.Collections.Generic;
using InstrumentCommunication.Application.Ports;
using QCRoutine.Adapter.Service_References.QCRoutineServiceReference;
using WCFServices.Common;

namespace QCRoutine.Adapter
{
    public class QCRoutineServicesAdapter : IQCRoutineServicePort
    {
        private readonly QCRoutineServiceClient client;

        public QCRoutineServicesAdapter()
        {
            client = new QCRoutineServiceClient();
        }

        public void StoreQCResult(QCResultDTO qcResultDto)
        {
            var qcResult = new QCResult
            {
                TestCode = qcResultDto.TestCode,
                Result = qcResultDto.Result,
                MeasuredDate = qcResultDto.MeasurementDate
            };

            var response = client.StoreQCResult(qcResult);
            if (response.Status == RequestResult.Error)
            {
                throw new ApplicationException("Error storing the QCResult.");
            }
        }

        public IList<QCResultDTO> GetQCResultsByDate(int amountOfResults)
        {
            throw new NotImplementedException();
        }
    }
}