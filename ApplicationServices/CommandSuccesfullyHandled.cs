namespace ApplicationServices
{
    public class CommandSuccesfullyHandled : IResponse
    {
        public CommandResult Status { get { return CommandResult.Success; } }
    }
}