namespace ApplicationServices
{
    public interface IResponse
    {
        CommandResult Status { get; }
        string Message { get; }
    }
}
