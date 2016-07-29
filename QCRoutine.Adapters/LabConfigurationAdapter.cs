using System;
using Application.Payloads;
using QCRoutine.Adapters.Service_References.LabConfigurationServiceReference;
using QCRoutine.Application.Ports;

namespace QCRoutine.Adapters
{
    public class LabConfigurationAdapter : ILabConfigurationPort
    {
        public ApplicationPayload GetApplicationByTestCode(int testCode)
        {
            var client = new LabConfigurationServiceClient();
            var response = client.GetApplicationByTestCode(testCode);
            if (response.Status==RequestResult.Succesfull)
            {
                return new ApplicationPayload(response.Application.TestCode);
               
            }
            return null;
        }
    }
}