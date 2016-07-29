using ApplicationServices;

namespace QCRoutine.Application.Commands
{
    public class GetResultsByDate : ICommand
    {
        public GetResultsByDate(int numberOfResults)
        {
            this.NumberOfResults = numberOfResults;
        }

        public int NumberOfResults { get; private set; }
    }
}