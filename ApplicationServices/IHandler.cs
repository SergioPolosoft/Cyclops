namespace ApplicationServices
{
    public interface IHandler<in Command,out Response> where Command:ICommand where Response:IResponse
    {
        Response Handle(Command command);
    }

    public interface IHandler<in Event>
        where Event : IEvent
    {
        void Handle(Event domainCommand);
    }
}