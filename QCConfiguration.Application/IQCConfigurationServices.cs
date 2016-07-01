using ApplicationServices;
using QCEvaluation.Domain;

namespace QCConfiguration.Application
{
    public interface IQCConfigurationServices:IHandler<ICommand,IResponse>, IHandler<IEvent>
    {        
    }
}