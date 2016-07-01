using System;
using ApplicationServices;
using InstrumentCommunication.Application.Commands;
using LabConfiguration.Application.Responses;
using LabConfiguration.Domain;

namespace LabConfiguration.Application.Commands.Handlers
{
    public class GetFTPConfigurationHandler:IHandler<GetFTPConfiguration, GetFTPConfigurationResponse>
    {
        public GetFTPConfigurationHandler(IConfigurationRepository configurationRepository)
        {
            throw new NotImplementedException();
        }

        public GetFTPConfigurationResponse Handle(GetFTPConfiguration command)
        {
            throw new NotImplementedException();
        }
    }
}