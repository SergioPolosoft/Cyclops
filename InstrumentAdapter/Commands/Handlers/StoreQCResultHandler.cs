using ApplicationServices;
using InstrumentCommunication.Application.Ports;

namespace InstrumentCommunication.Application.Commands.Handlers
{
    public class StoreQCResultHandler:IHandler<StoreQCResult,IResponse>
    {
        private readonly IQCRoutineServicePort qcRoutineServices;

        public StoreQCResultHandler(IQCRoutineServicePort qcRoutineServices)
        {
            this.qcRoutineServices = qcRoutineServices;
        }

        public IResponse Handle(StoreQCResult command)
        {
            qcRoutineServices.StoreQCResult(new QCResultDTO(command.TestCode, command.Result, command.MeasuredDate));
            return new CommandSuccesfullyHandled();
        }
    }
}