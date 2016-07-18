using System;
using Application.Payloads;
using ApplicationServices;
using InstrumentCommunication.Application.Commands;
using LabConfiguration.Application.Responses;
using LabConfiguration.Domain;
using FTPConfigurationPayload = Application.Payloads.FTPConfigurationPayload;

namespace LabConfiguration.Application.Commands.Handlers
{
    public class GetFTPConfigurationHandler:IHandler<GetFTPConfiguration, GetFTPConfigurationResponse>
    {
        private readonly IConfigurationRepository configurationRepository;

        public GetFTPConfigurationHandler(IConfigurationRepository configurationRepository)
        {
            this.configurationRepository = configurationRepository;
        }

        public GetFTPConfigurationResponse Handle(GetFTPConfiguration command)
        {
            GetFTPConfigurationResponse getFtpConfigurationResponse;
            try
            {
                var configuration = this.configurationRepository.GetFTPConfiguration();

                getFtpConfigurationResponse = new GetFTPConfigurationResponse
                {
                    FTPConfiguration = new FTPConfigurationPayload(configuration)
                };
            }
            catch (Exception)
            {
                getFtpConfigurationResponse = new GetFTPConfigurationFailed();
            }
            

            return getFtpConfigurationResponse;
        }
    }
}