using System;
using LabConfiguration.Adapters.QCEvaluationServiceReference;
using LabConfiguration.Application;

namespace LabConfiguration.Adapters
{
    public class QCEvaluationAdapter : IQCEvaluationPort
    {
        public void NotifyApplicationInstalled(int applicationCode)
        {
            try
            {
                var qcEvaluationService = new QCEvaluationServiceClient();
                qcEvaluationService.NotifyApplicationInstalled(applicationCode);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
            
        }
    }
}