using ApplicationServices;

namespace QCConfiguration.Application
{
    public interface IQCConfigurationServices:IHandler<ICommand,IResponse>, IHandler<IEvent>
    {        
    }
}