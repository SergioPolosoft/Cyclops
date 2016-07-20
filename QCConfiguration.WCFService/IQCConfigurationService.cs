using System.ServiceModel;

namespace QCConfiguration.WCFService
{
    [ServiceContract]
    public interface IQcConfigurationService
    {
        [OperationContract]
        GetQualityControlResponse GetQualityControl(GetQualityControlRequest request);
    }
}