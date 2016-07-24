using System.ServiceModel;

namespace LabConfiguration.WCFService
{
    [ServiceContract]
    public interface ILabConfigurationService
    {
        [OperationContract]
        ConfirmApplicationInstallationResponse ConfirmApplicationInstallation(
            ConfirmApplicationInstallationRequest request);
    }
}