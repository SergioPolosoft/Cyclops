using System.Collections.Generic;

namespace InstrumentCommunication.Application.Ports
{
    public interface IQCRoutineServicePort
    {
        void StoreQCResult(QCResultDTO qcResultDto);
        IList<QCResultDTO> GetQCResultsByDate(int amountOfResults);
    }
}