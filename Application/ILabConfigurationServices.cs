using System;
using ApplicationServices;
using LabConfiguration.Domain;

namespace LabConfiguration.Application
{
    public interface ILabConfigurationServices:IHandler<ICommand,IResponse>
    {       
    }
}