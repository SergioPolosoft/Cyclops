using System.Collections.Generic;
using ApplicationServices;


namespace QCRoutine.Application
{
    public interface IQCRoutineServices:IHandler<ICommand,IResponse>
    {
    }
}