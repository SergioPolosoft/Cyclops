using System;
using System.Collections.Generic;
using ApplicationServices;
using Infrastructure.DTOs;
using InstrumentAdapter.Domain;
using InstrumentCommunication.Application.Commands;
using InstrumentCommunication.Application.Commands.Handlers;
using InstrumentCommunication.Application.Events.Handlers;
using InstrumentCommunication.Application.Ports;
using InstrumentCommunication.Sender;
using InstrumentCommunication.TsnAdapter;
using InstrumentCommunication.TsnAdapter.Events;
using ILabConfigurationPort = InstrumentCommunication.Application.Ports.ILabConfigurationPort;
using StoreQCResult = InstrumentCommunication.Application.Commands.StoreQCResult;
using StoreQCResultHandler = InstrumentCommunication.Application.Commands.Handlers.StoreQCResultHandler;

namespace InstrumentCommunication.Application
{
    public class InstrumentCommunicationServices : ApplicationServicesBase
    {
        private readonly ICommunicationStatusRepository communicationStatusRepository;
        private readonly IMessagesRepository messageRepository;
        private readonly IMessageSender messageSender;
        private readonly ITSNAdapter tsnAdapter;        
        private readonly ILabConfigurationPort labConfigurationAdapter;
        private readonly IQCRoutineServicePort qcRoutineServices;

        public InstrumentCommunicationServices(ICommunicationStatusRepository communicationStatusRepository, ITSNAdapter tsnAdapter, IMessageSender messageSender, IMessagesRepository messageRepository, ILabConfigurationPort labConfigurationAdapter, IQCRoutineServicePort qcRoutineServices)
        {
            this.communicationStatusRepository = communicationStatusRepository;
            this.messageRepository = messageRepository;
            this.labConfigurationAdapter = labConfigurationAdapter;
            this.qcRoutineServices = qcRoutineServices;
            this.messageSender = messageSender;
            this.tsnAdapter = tsnAdapter;

            tsnAdapter.Publish += this.Handle;
        }

        public void EstablishCommunication()
        {
            this.Handle(new EstablishCommunicationCommand());            
        }

        public CommunicationStatusDTO GetStatus()
        {
            switch (communicationStatusRepository.GetCurrentStatus())
            {
                case CommunicationStatus.WaitingForAcknowledge:
                    return CommunicationStatusDTO.WaitingForAcknowledge;
                case CommunicationStatus.WaitingForMessages:
                    return CommunicationStatusDTO.WaitingForMessages;
                default:
                    throw new UnknownCommunicationStatusException();
            }
        }

        protected override Dictionary<Type, Action<IEvent>> GetEventHandlers()
        {
            return new Dictionary<Type, Action<IEvent>>
            {
                {
                    typeof (UrgentInformationReceived),
                    x => new UrgentInformationReceivedHandler(messageSender).Handle(x as UrgentInformationReceived)
                        
                },
                {
                    typeof (ListOfParametersReceived),
                    x => new ParametersListReceivedHandler(messageSender).Handle(x as ListOfParametersReceived)
                        
                },
                {
                    typeof (ParameterReceived),
                    x => new ParameterReceivedHandler(messageSender).Handle(x as ParameterReceived)
                        
                },

            };
        }

        protected override Dictionary<Type, Func<ICommand,IResponse>> GetCommandHandlers()
        {
            return new Dictionary<Type, Func<ICommand, IResponse>>
            {
                {
                    typeof (UrgentInformationRequestCommand),
                    x =>
                        new StoreMessageHandler(messageRepository,x as UrgentInformationRequestCommand).Handle(new ActionCommand(()=>
                            new ConfirmReceptionHandler(messageSender).Handle(new ActionCommand(()=>new DownloadUrgentInformationHandler(tsnAdapter).Handle(x as UrgentInformationRequestCommand)))))
                },
                {
                    typeof (StoreMainUnitFTPCredentialsCommand),
                    x => new ConfirmReceptionHandler(messageSender).Handle(new ActionCommand(()=> 
                        new StoreMainUnitFtpCredentialsHandler(labConfigurationAdapter).Handle(
                            x as StoreMainUnitFTPCredentialsCommand)))
                },
                {
                    typeof (ConfirmMessageReceptionCommand),
                    x =>
                        new ConfirmMessageReceptionHandler(communicationStatusRepository).Handle(
                            x as ConfirmMessageReceptionCommand)
                }
                ,
                {
                    typeof(EstablishCommunicationCommand),
                    x=> new EstablishCommunicationHandler(communicationStatusRepository, messageSender,labConfigurationAdapter).Handle(x as EstablishCommunicationCommand)
                },
                {
                    typeof (GetParameterListCommand),
                    x => new ConfirmReceptionHandler(messageSender).Handle(new ActionCommand(()=>  
                        new GetParameterListHandler(tsnAdapter).Handle(x as GetParameterListCommand)))
                },
                {
                    typeof(GetParameterCommand), x=> new ConfirmReceptionHandler(messageSender).Handle(new ActionCommand(()=>  new GetParameterHandler(tsnAdapter).Handle(x as GetParameterCommand)))
                },
                {
                    typeof(ParameterConfirmationCommand), x=> new ConfirmReceptionHandler(messageSender).Handle(new ActionCommand(()=> new ParameterConfirmationHandler(labConfigurationAdapter).Handle(x as ParameterConfirmationCommand)))
                },
                {
                    typeof(StoreQCResult), x=> new StoreQCResultHandler(qcRoutineServices).Handle(x as StoreQCResult)
                },
                

            };
        }
    }
}