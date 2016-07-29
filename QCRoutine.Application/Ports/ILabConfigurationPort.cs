using Application.Payloads;

namespace QCRoutine.Application.Ports
{
    public interface ILabConfigurationPort
    {
        ApplicationPayload GetApplicationByTestCode(int testCode);
    }
}