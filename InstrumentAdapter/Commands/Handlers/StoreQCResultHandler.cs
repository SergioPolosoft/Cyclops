using ApplicationServices;
using QCRoutine.Application;
using QCRoutine.Application.Commands;

namespace InstrumentCommunication.Application.Commands.Handlers
{
    public class StoreQCResultHandler:IHandler<StoreQCResult,IResponse>
    {
        private readonly IQCRoutineServices qcRoutineServices;

        public StoreQCResultHandler(IQCRoutineServices qcRoutineServices)
        {
            this.qcRoutineServices = qcRoutineServices;
        }

        public IResponse Handle(StoreQCResult command)
        {
            qcRoutineServices.Handle(command);
            return new CommandSuccesfullyHandled();
        }
    }
}