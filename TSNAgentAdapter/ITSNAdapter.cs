using System;
using ApplicationServices;

namespace InstrumentCommunication.TsnAdapter
{
    public interface ITSNAdapter
    {        
        void Handle(ICommand command);

        event Action<IEvent> Publish;
    }
}