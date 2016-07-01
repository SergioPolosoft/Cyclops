namespace ApplicationServices
{
    public class CommandNotHandled : IResponse
    {
        private string reason;

        public CommandNotHandled(string reason)
        {
            this.reason = reason;
        }

        public CommandResult Status
        {
            get { return CommandResult.NotHandled; }
        }
    }
}