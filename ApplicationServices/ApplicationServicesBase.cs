using System;
using System.Collections.Generic;

namespace ApplicationServices
{
    public abstract class ApplicationServicesBase:IHandler<ICommand,IResponse>,IHandler<IEvent>
    {
        private Dictionary<Type, Func<ICommand, IResponse>> commandHandlers;

        private Dictionary<Type, Func<ICommand, IResponse>> CommandHandlers
        {
            get
            {
                if (this.commandHandlers == null)
                {
                    this.commandHandlers = GetCommandHandlers();
                }
                return commandHandlers;
            }   
        }

        protected abstract Dictionary<Type, Func<ICommand, IResponse>> GetCommandHandlers();

        public IResponse Handle(ICommand command)
        {
            return CommandHandlers[command.GetType()].Invoke(command);
        }

        protected abstract Dictionary<Type, Action<IEvent>> GetEventHandlers();

        public void Handle(IEvent @event)
        {
            EventHandlers[@event.GetType()].Invoke(@event);
        }

        private Dictionary<Type, Action<IEvent>> eventHandlers;

        private Dictionary<Type, Action<IEvent>> EventHandlers
        {
            get
            {
                if (this.eventHandlers == null)
                {
                    this.eventHandlers = GetEventHandlers();
                }
                return eventHandlers;
            }
        }

       
    }
}