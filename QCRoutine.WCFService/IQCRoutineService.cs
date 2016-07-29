using System.ServiceModel;

namespace QCRoutine.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IQCRoutineService" in both code and config file together.
    [ServiceContract]
    public interface IQCRoutineService
    {
        [OperationContract]
        StoreQCResultResponse StoreQCResult(QCResult request);
    }
}
