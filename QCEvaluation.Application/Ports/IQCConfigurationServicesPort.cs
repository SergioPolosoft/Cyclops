using Application.Payloads;

namespace QCEvaluation.Application.Ports
{
    public interface IQCConfigurationServicesPort
    {
        QualityControlPayload GetQualityControl(int testCode);
    }
}