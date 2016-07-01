using ApplicationServices;
using InstrumentAdapter.Domain;

namespace InstrumentCommunication.Application.Commands
{
    public interface ICommandWithMessage:ICommand
    {
        Message Message { get; }
    }
}