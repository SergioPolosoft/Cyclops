using ApplicationServices;
using ICommand = ApplicationServices.ICommand;

namespace QCEvaluation.Application
{
    public interface IQCEvaluationServices:IHandler<ICommand,IResponse>, IHandler<IEvent>
    {
    }
}