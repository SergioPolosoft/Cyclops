﻿using Application.Payloads;
using QCEvaluation.Adapters.Service_References.QCConfigurationServiceReference;
using QCEvaluation.Application.Ports;

namespace QCEvaluation.Adapters
{
    public class QCConfigurationServicesAdapter : IQCConfigurationServicesPort
    {
        public QualityControlPayload GetQualityControl(int testCode)
        {
            var client = new QcConfigurationServiceClient();
            var response = client.GetQualityControl(new GetQualityControlRequest(){TestCode = testCode});
            if (response.QualityControl!=null)
            {
                var payload = new QualityControlPayload();
                var qualityControl = response.QualityControl;
                payload.TestCode = qualityControl.TestCode;
                payload.StandardDeviation = qualityControl.StandardDeviation;
                payload.TargetValue = qualityControl.TargetValue;
                return payload;
            }
            return null;
        }
    }
}