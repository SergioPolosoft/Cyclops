namespace QCEvaluation.Application.Commands.Handlers
{
    public interface IBuilder<T> where T:new()
    {
        T Object { get; }
    }
}